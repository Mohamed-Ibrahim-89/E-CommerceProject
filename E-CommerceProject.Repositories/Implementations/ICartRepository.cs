using E_CommerceProject.Entities.Models;

namespace E_CommerceProject.Repositories.Implementations
{
    public interface ICartRepository
    {
        Task AddToCart(Product product);
        Task RemoveFromCart(Product product);
        Task<List<Cart>> GetCartItems();
        Task ClearCart();
        Task<decimal> GetCartTotal();
    }
}
