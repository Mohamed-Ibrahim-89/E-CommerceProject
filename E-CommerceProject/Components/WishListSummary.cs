namespace E_CommerceProject.Components;

public class WishListSummary(IWishListRepository wishListRepository, IHttpContextAccessor contextAccessor) : ViewComponent
{
    private readonly IWishListRepository _wishListRepository = wishListRepository;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = await GetSignedUserId();
        var totalCount = await _wishListRepository.GetTotalCount(userId, CancellationToken.None);
        return View(totalCount);
    }

    private async Task<string> GetSignedUserId()
    {
        return _contextAccessor!.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }
}
