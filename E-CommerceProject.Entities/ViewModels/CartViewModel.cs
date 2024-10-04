using E_CommerceProject.Entities.Models;

namespace E_CommerceProject.Entities.ViewModels
{
    public class CartViewModel(List<Cart> cartItems, int cartTotal)
    {
        public List<Cart> CartItems { get; } = cartItems;
        public int CartTotal { get; } = cartTotal;

    }
}
