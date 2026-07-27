namespace E_CommerceProject.Controllers;

public class BaseController : Controller
{
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
        };
    }
}
