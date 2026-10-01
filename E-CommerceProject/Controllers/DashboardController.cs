using E_CommerceProject.Entities.Constants;

namespace E_CommerceProject.Controllers;

[Authorize(Roles = Constants.Roles.Admin)]
public class DashboardController(AppDbContext context
    ,IToastNotification toastNotification
    ,IHttpContextAccessor contextAccessor)
    : BaseController(contextAccessor)
{
    private readonly AppDbContext _context = context;
    private readonly IToastNotification _toastNotification = toastNotification;

    public async Task<IActionResult> Index(CancellationToken token)
    {
        try
        {
            var model = new DashboardViewModel
            {
                TotalProducts = await _context.Products.CountAsync(token),
                TotalCategories = await _context.Categories.CountAsync(token),
                TotalOrders = await _context.Orders.CountAsync(token),
                TotalUsers = await _context.Users.CountAsync(token),
                PendingOrders = await _context.Orders
                    .CountAsync(o => o.Status == OrderStatus.Pending, token),
                LowStockProducts = await _context.Products
                    .CountAsync(p => p.QuantityInStock <= 5, token),
                TotalRevenue = await _context.Orders
                    .Where(o => o.Status == OrderStatus.Delivered)
                    .SumAsync(o => (decimal?)o.TotalPrice, token) ?? 0m
            };

            return View(model);
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View("Error");
        }
    }
}
