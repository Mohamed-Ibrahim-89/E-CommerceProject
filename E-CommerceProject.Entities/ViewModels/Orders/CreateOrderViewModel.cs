namespace E_CommerceProject.Entities.ViewModels.Orders;

public class CreateOrderViewModel : CustomerInfoViewModel
{
    public decimal? TotalPrice { get; set; }

    public int? CustomerInfoId { get; set; }

    public string? UserId { get; set; } = string.Empty;

    public List<OrderDetailViewModel>? OrderDetails { get; set; }
}
