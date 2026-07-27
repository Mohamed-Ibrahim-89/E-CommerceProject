using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace E_CommerceProject.Repositories.Services;

public interface ICacheService
{
    /// <summary>
    /// Gets the value associated with the specified key from the cache. If the key does not exist,
    /// it uses the provided factory function to create the value, stores it in the cache, and returns it.
    /// <typeparam name="T">The type of the value to get or set.</typeparam>
    /// <param name="key">The key of the value to get or set.</param>
    /// <param name="factory">The function to create the value if the key does not exist.</param>
    /// <param name="expiration">The expiration time for the cached value.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The value associated with the specified key.</returns>
    /// </summary>
    Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the value associated with the specified key in the cache with an optional expiration time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync<T>(string key, T value,
        TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T?> GetTAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the value associated with the specified key from the cache.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes all values from the cache that match the specified prefix.
    /// </summary>
    Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default);
}


public class MemoryCacheService(IMemoryCache cache) : ICacheService
{
    private readonly IMemoryCache _cache = cache;
    private readonly ConcurrentDictionary<string, byte> _keys = new();
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(1);

    public async Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return (await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);
            entry.RegisterPostEvictionCallback((k, _, _, _) =>
            {
                _keys.TryRemove(k.ToString()!, out _);
            });

            _keys.TryAdd(key, 0);
            return await factory(cancellationToken);

        })) is T value ? value : default!;
    }
    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var options = new MemoryCacheEntryOptions();

        options.SetAbsoluteExpiration(expiration ?? DefaultExpiration);
        options.RegisterPostEvictionCallback((k, _, _, _) =>
            _keys.TryRemove(k.ToString()!, out _));

        _cache.Set(key, value, options);
        return Task.CompletedTask;
    }

    public Task<T?> GetTAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _cache.TryGetValue(key, out T? value);
        return Task.FromResult(value);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _cache.Remove(key);
        _keys.TryRemove(key, out _);

        return Task.CompletedTask;
    }

    public Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        foreach (var key in _keys.Keys.Where(k => k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
        {
            _cache.Remove(key);
            _keys.TryRemove(key, out _);
        }

        return Task.CompletedTask;
    }

}