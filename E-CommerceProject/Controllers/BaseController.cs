namespace E_CommerceProject.Controllers;

public class BaseController(IHttpContextAccessor contextAccessor) : Controller
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    protected DataTableParamsViewModel GetDatatableParamsFromRequest()
    {
        return new DataTableParamsViewModel
        {
            Draw = Convert.ToInt32(HttpContext.Request.Query[Constants.DataTableParams.Draw])!,
            SearchValue = HttpContext.Request.Query[Constants.DataTableParams.Search]!,
            Start = HttpContext.Request.Query[Constants.DataTableParams.Start]!,
            Length = HttpContext.Request.Query[Constants.DataTableParams.Length]!,
            SortColumn = HttpContext.Request.Query[Constants.DataTableParams.Order]!,
            SortColumnDirection = HttpContext.Request.Query[Constants.DataTableParams.OrderDir]!,
            CategoryId = HttpContext.Request.Query[Constants.DataTableParams.CategoryId]!,
        };
    }
    protected async Task<string> GetSignedUserId()
    {
        var userId = _contextAccessor!.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? throw new UnauthorizedAccessException("User not exist");

        return userId!;
    }
}
