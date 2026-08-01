namespace E_CommerceProject.Repositories.Repositories;

public interface ICartRepository
{
    /// <summary>
    /// Get cart items
    /// </summary>
    /// <returns></returns>
    Task<List<CartViewModel>> GetCartItems(string userId, CancellationToken token);
    /// <summary>
    /// Add product to cart
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task AddToCart(int productId, string userId, CancellationToken token);
    /// <summary>
    /// Remove product from cart
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task RemoveFromCart(string userId, int productId, CancellationToken token);
    /// <summary>
    /// Clear cart
    /// </summary>
    Task ClearCart(string userId, CancellationToken token);
    /// <summary>
    /// Get Total Cart Items
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<TotalCountViewModel> GetTotalCount(string userId, CancellationToken token);
}
public class CartRepository(AppDbContext context) : ICartRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<CartViewModel>> GetCartItems(string userId, CancellationToken token)
    {
        var cartTotal = await GetCartTotalCost(userId, token);

        var model = await _context.Carts.Where(
            c => c.UserId == userId)
            .Include(p => p.Product)
            .ThenInclude(d => d!.Discount)
            .Select(c => new CartViewModel
            {
                ProductId = c.ProductId,
                ProductName = c.Product!.Name,
                ProductCover = c.Product.Cover,
                ProductPrice = c.Product.Price,
                ProductDiscount = c.Product.Discount!.Percentage,
                Amount = c.Amount,
                CartTotal = cartTotal
            })
            .ToListAsync(token);

        return model;
    }

    public async Task AddToCart(int productId, string userId, CancellationToken token)
    {
        if(await ProductExist(productId))
        {
            var CartItem = await _context.Carts
                .FirstOrDefaultAsync(s => s.Product!.Id == productId && s.UserId == userId);

            if (CartItem == null)
            {
                CartItem = new Cart
                {
                    Amount = 1,
                    ProductId = productId,
                    UserId = userId,
                };
                await _context.Carts.AddAsync(CartItem, token);
            }
            else
                CartItem.Amount++;

            await _context.SaveChangesAsync();
        }
        else 
            throw new InvalidOperationException("Product does not exist!");
    }

    public async Task RemoveFromCart(string userId, int productId, CancellationToken token)
    {
        if (await ProductExist(productId))
        {
            var CartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.Product!.Id == productId && c.UserId == userId);

            var localAmount = 0;
            if (CartItem != null)
            {
                if (CartItem.Amount > 1)
                {
                    CartItem.Amount--;
                    localAmount = CartItem.Amount;
                }
                else
                {
                    _context.Carts.Remove(CartItem);
                }
            }
            await _context.SaveChangesAsync();
        }
        else throw new InvalidOperationException("Product does not exsit!");
    }

    public async Task ClearCart(string userId, CancellationToken token)
    {
        var cartItems = await _context.Carts
            .Where(c => c.UserId == userId)
            .ToListAsync();

        _context.Carts.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
    }

    public async Task<TotalCountViewModel> GetTotalCount(string userId, CancellationToken token)
    {
        var totalCount = await _context.Carts
        .Where(c => c.UserId == userId)
        .SumAsync(c => c.Amount);

        return new TotalCountViewModel
        {
            TotalCount = totalCount
        };
    }

    private async Task<decimal> GetCartTotalCost(string userId, CancellationToken token) 
        => await _context.Carts
            .Where(c => c.UserId == userId)
            .Include(p => p.Product)
            .ThenInclude(d => d!.Discount)
            .Select(c => 
                (c.Product!.Price - c.Product.Price * c.Product.Discount!.Percentage / 100) * c.Amount)
            .SumAsync(token);

    private async Task<bool> ProductExist(int productId) => await _context.Products.AnyAsync(p => p.Id == productId);
}
