using E_CommerceProject.Entities.Models;
using E_CommerceProject.Entities.ViewModels;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace E_CommerceProject.Controllers
{
    public class CartController(IBaseRepository<Product> productRepository, ICartRepository cartRepository) : Controller
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IBaseRepository<Product> _productRepository = productRepository;


        public async Task<IActionResult> Index()
        {
            var cartItem = await _cartRepository.GetCartItems();
            var cartTotal = await _cartRepository.GetCartTotal();

            var cartViewModel = new CartViewModel(cartItem, cartTotal);

            return View(cartViewModel);
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            var product = await _productRepository.GetById(p => p.ProductId == productId, ["Discount"]);

            if (product != null)
            {
                await _cartRepository.AddToCart(product);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var product = await _productRepository.GetById(p => p.ProductId == productId);

            if (product != null)
            {
                await _cartRepository.RemoveFromCart(product);
            }

            return RedirectToAction("Index");
        }
    }
}
