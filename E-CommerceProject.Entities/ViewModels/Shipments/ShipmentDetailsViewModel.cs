namespace E_CommerceProject.Entities.ViewModels.Shipments;

public class ShipmentDetailsViewModel : AdminShipmentViewModel
{
    // Customer Information
    [Display(Name = "Address Line 1")]
    public string AddressLine1 { get; set; } = string.Empty;
    [Display(Name = "Address Line 2")]
    public string AddressLine2 { get; set; } = string.Empty; // + Customer Name & Phone Number

    // Order Information
    [Display(Name = "Order Date")]
    public DateTime OrderDate { get; set; }
    [Display(Name = "Total Price")]
    public decimal TotalPrice { get; set; } // + Status

    // Order Details
    public List<OrderDetailViewModel> OrderDetails { get; set; } = new();
}

public class OrderDetailViewModel
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}