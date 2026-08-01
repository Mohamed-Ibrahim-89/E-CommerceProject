namespace E_CommerceProject.Repositories.Repositories;

public interface IWishListRepository
{
    /// <summary>
    /// Get wishlist items
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<WishListViewModel>> GetWishListItems(string userId, CancellationToken token);
    /// <summary>
    /// Add product to wishlist
    /// </summary>
    /// <param name="product"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task AddToWishList(int productId, string userId, CancellationToken token);
    /// <summary>
    /// Remove product from wishlist
    /// </summary>
    /// <param name="product"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task RemoveFromWishList(int productId, string userId, CancellationToken token);
    /// <summary>
    /// Clear wishlist
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task ClearWishList(string userId, CancellationToken token);
    /// <summary>
    /// Get total count of wishlist items
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<TotalCountViewModel> GetTotalCount(string userId, CancellationToken token);
}

public class WishListRepository(AppDbContext context) : IWishListRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<WishListViewModel>> GetWishListItems(string userId, CancellationToken token)
    {
        return await _context.Wishlists
            .Where(w => w.UserId == userId)
            .Include(p => p.Product)
            .ThenInclude(d => d!.Discount)
            .Select(w => new WishListViewModel
            {
                ProductId = w.ProductId,
                ProductName = w.Product!.Name,
                ProductCover = w.Product.Cover,
                ProductPrice = w.Product.Price,
                ProductDiscount = w.Product.Discount!.Percentage
            })
            .ToListAsync();
    }

    public async Task AddToWishList(int productId, string userId, CancellationToken token)
    {
        if (await ProductExist(productId))
        {
            var wishListItems = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.Product!.Id == productId && w.UserId == userId);

            if (wishListItems == null)
            {
                wishListItems = new Wishlist
                {
                    UserId = userId,
                    ProductId = productId
                };
                await _context.Wishlists.AddAsync(wishListItems);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Item already exist in wishList!");
            }
        }
        else
        {
            throw new Exception("Product does not exist!");
        }
    }

    public async Task RemoveFromWishList(int productId, string userId, CancellationToken token)
    {
        if (await ProductExist(productId))
        {
            var wishListItem = await _context.Wishlists
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (wishListItem != null)
            {
                _context.Wishlists.Remove(wishListItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Item does not exsit!");
            }
        }
        else
        {
            throw new Exception("Product does not exsit!");
        }
    }
    
    public async Task ClearWishList(string userId, CancellationToken token)
    {
        var wishListItems = await _context.Wishlists
            .Where(w => w.UserId == userId)
            .ToListAsync(token);

        _context.Wishlists.RemoveRange(wishListItems);
        await _context.SaveChangesAsync();
    }

    public async Task<TotalCountViewModel> GetTotalCount(string userId, CancellationToken token)
    {
        var totalCount = await _context.Wishlists
            .Where(w => w.UserId == userId)
            .CountAsync(token);
        return new TotalCountViewModel
        {
            TotalCount = totalCount
        };
    }

    private async Task<bool> ProductExist(int productId) => await _context.Products.AnyAsync(p => p.Id == productId);
}
