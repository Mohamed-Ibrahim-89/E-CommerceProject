using E_CommerceProject.Entities.Models;

namespace E_CommerceProject.Repositories.Interfaces
{
    public interface IWishListRepository
    {
        Task AddToWishList(Product product, string userId);
        Task RemoveFromWishList(Product product, string userId);
        Task<List<Wishlist>> GetWishListItems(string userId);
        Task ClearWishList(string userId);
    }
}
