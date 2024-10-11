using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Data;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Repositories.Implementations
{
    public class WishListRepository(AppDbContext context) : IWishListRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddToWishList(Product product, string userId)
        {
            var wishListItems = await _context.Wishlists.FirstOrDefaultAsync(w => w.Product.ProductId == product.ProductId && w.AppUserId == userId);

            if (wishListItems == null)
            {
                wishListItems = new Wishlist
                {
                    AppUserId = userId,
                    ProductId = product.ProductId
                };
                await _context.Wishlists.AddAsync(wishListItems);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Item already exist in wishList!");
            }
        }

        public async Task ClearWishList(string userId)
        {
            var wishListItems = await _context.Wishlists.Where(w => w.AppUserId == userId).ToListAsync();

            _context.Wishlists.RemoveRange(wishListItems);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Wishlist>> GetWishListItems(string userId)
        {
            return await _context.Wishlists.Where(w => w.AppUserId == userId).Include(p => p.Product).ThenInclude(d => d.Discount).ToListAsync();
        }

        public async Task RemoveFromWishList(Product product, string userId)
        {
            var wishListItem = await _context.Wishlists.FirstOrDefaultAsync(
                s => s.AppUserId == userId);

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
    }
}
