namespace E_CommerceProject.Controllers;

[Authorize]
public class WishListController(IHttpContextAccessor contextAccessor
    ,IWishListRepository wishListRepository
    ,IToastNotification toastNotification
    ,IBaseRepository<Product> productRepository) : BaseController(contextAccessor)
{
    private readonly IWishListRepository _repository = wishListRepository;
    private readonly IBaseRepository<Product> _productRepository = productRepository;
    private readonly IToastNotification _toastNotification = toastNotification;

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var userId = await GetSignedUserId();
        var viewModel = await _repository.GetWishListItems(userId, token);

        return View(viewModel);
    }

    public async Task<IActionResult> AddToWishList(int productId, CancellationToken token)
    {
        try { 
            var userId = await GetSignedUserId();
            await _repository.AddToWishList(productId, userId, token);

            _toastNotification.AddSuccessToastMessage("Product added to wishlist successfully!");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) 
        {
            _toastNotification.AddWarningToastMessage(ex.Message);
            return RedirectToAction(nameof(Index), "Home");
        }
    }

    public async Task<IActionResult> RemoveFromWishList(int productId, CancellationToken token)
    {
        try
        {
            var userId = await GetSignedUserId();
            await _repository.RemoveFromWishList(productId, userId, token);
        }
        catch (Exception ex)
        {
            _toastNotification.AddWarningToastMessage(ex.Message);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveAllWishList(CancellationToken token)
    {
        try
        {
            var userId = await GetSignedUserId();
            await _repository.ClearWishList(userId, token);
        }
        catch (Exception ex)
        {
            _toastNotification.AddWarningToastMessage(ex.Message);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}
