using E_CommerceProject.Entities.ViewModels;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Components
{
    public class ShoppingCartSummary(ICartRepository shoppingCart) : ViewComponent
    {
        private readonly ICartRepository _shoppingCart = shoppingCart;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _shoppingCart.GetCartItems();
            var cartTotal = await _shoppingCart.GetCartTotal();

            var shoppingCartViewModel = new CartViewModel(items, cartTotal);
            return View(shoppingCartViewModel);
        }
    }
}
