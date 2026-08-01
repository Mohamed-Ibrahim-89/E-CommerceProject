namespace E_CommerceProject.Controllers;

[Authorize]
public class CartController(ICartRepository repository, IHttpContextAccessor contextAccessor) : BaseController(contextAccessor)
{
    private readonly ICartRepository _repository = repository;

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var userId = await GetSignedUserId();
        var viewModels = await _repository.GetCartItems(userId, token);

        return View(viewModels);
    }

    public async Task<IActionResult> AddToCart(int productId, CancellationToken token)
    {
        var userId = await GetSignedUserId();
        await _repository.AddToCart(productId, userId, token);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveFromCart(int productId, CancellationToken token)
    {
        var userId = await GetSignedUserId();
        await _repository.RemoveFromCart(userId, productId, token);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ClearCart(CancellationToken token)
    {
        var userId = await GetSignedUserId();
        await _repository.ClearCart(userId, token);
        return RedirectToAction(nameof(Index));
    }
}
