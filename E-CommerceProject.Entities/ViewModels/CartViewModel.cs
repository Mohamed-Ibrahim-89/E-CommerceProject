namespace E_CommerceProject.Entities.ViewModels;

public class CartViewModel(List<Cart> cartItems, decimal cartTotal)
{
    public List<Cart> CartItems { get; } = cartItems;
    public decimal CartTotal { get; } = cartTotal;

}
