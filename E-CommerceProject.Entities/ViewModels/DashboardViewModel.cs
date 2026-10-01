namespace E_CommerceProject.Entities.ViewModels;

public class DashboardViewModel
{
    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
    public int TotalOrders { get; set; }
    public int TotalUsers { get; set; }
    public int PendingOrders { get; set; }
    public int LowStockProducts { get; set; }
    public decimal TotalRevenue { get; set; }
}

public class  TotalCountViewModel
{
    public int TotalCount { get; set; }
}