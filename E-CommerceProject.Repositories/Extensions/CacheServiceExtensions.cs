namespace E_CommerceProject.Repositories.Extensions;

public static class CacheServiceExtensions
{
    public static IServiceCollection AddCacheService(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
        return services;
    }
}
