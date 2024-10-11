using E_CommerceProject.Entities.Models;
using E_CommerceProject.Entities.ViewModels;
using E_CommerceProject.Repositories.Implementations;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace E_CommerceProject.Controllers
{
    [Authorize]
    public class WishListController(IHttpContextAccessor contextAccessor, IWishListRepository wishListRepository, UserManager<AppUser> userManager, IToastNotification toastNotification, IBaseRepository<Product> productRepository) : Controller
    {
        private readonly IWishListRepository _wishListRepository = wishListRepository;
        private readonly IBaseRepository<Product> _productRepository = productRepository;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly IToastNotification _toastNotification = toastNotification;


        public async Task<IActionResult> Index()
        {
            var userId = await GetSignedUserId();
            var wishList = await _wishListRepository.GetWishListItems(userId);

            var wishListViewModel = new WishListViewModel(wishList);


            return View(wishListViewModel);
        }

        public async Task<IActionResult> AddToWishList(int productId)
        {
            var product = await _productRepository.GetById(p => p.ProductId == productId, ["Discount"]);

            try
            {
                if (product != null)
                {
                    var userId = await GetSignedUserId();

                    await _wishListRepository.AddToWishList(product, userId);
                }
                _toastNotification.AddSuccessToastMessage("Product added to wishlist successfully!");
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                _toastNotification.AddWarningToastMessage(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> RemoveFromWishList(int productId)
        {
            var product = await _productRepository.GetById(p => p.ProductId == productId);

            if (product != null)
            {
                var userId = await GetSignedUserId();

                await _wishListRepository.RemoveFromWishList(product, userId);
            }
            return RedirectToAction("Index");
        }

        public async Task<string> GetSignedUserId()
        {
            var username = _contextAccessor!.HttpContext!.User.Identity!.Name;
            var user =  await _userManager.FindByNameAsync(username!);

            return user!.Id;
        }
    }
}
