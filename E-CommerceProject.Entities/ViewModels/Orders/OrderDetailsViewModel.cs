using E_CommerceProject.Entities.ViewModels.Shipments;

namespace E_CommerceProject.Entities.ViewModels.Orders;

public class OrderDetailsViewModel
{
    // Order Information
    public OrderStatus Status { get; set; }
    [Display(Name = "Order Date")]
    public DateTime OrderDate { get; set; }
    [Display(Name = "Total Price")]
    public decimal TotalPrice { get; set; }
    // Customer Information
    public string Name { get; set; } = string.Empty;
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Display(Name = "Address Line 1")]
    public string AddressLine1 { get; set; } = string.Empty;
    [Display(Name = "Address Line 2")]
    public string AddressLine2 { get; set; } = string.Empty;
    // Order Details
    public List<OrderDetailViewModel> OrderDetails { get; set; } = [];
}
