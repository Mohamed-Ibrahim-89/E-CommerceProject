namespace E_CommerceProject.Entities.ViewModels.Shipments;

public class ShipmentViewModel
{
    public int Id { get; set; }
    public string Carrier { get; set; } = string.Empty;

    [Display(Name = "Tracking Number")]
    public string TrackingNumber { get; set; } = string.Empty;

    [Display(Name = "Shipping Date")]
    public DateTime ShippingDate { get; set; }

    [Display(Name = "Estimated Delivery Date")]
    public DateTime EstimatedDeliveryDate { get; set; }

    [Display(Name = "Shipping Cost")]
    public decimal ShippingCost { get; set; }

    public OrderStatus Status { get; set; }

    public string StatusName => Status.ToString();
}

public class AdminShipmentViewModel : ShipmentViewModel
{
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; }
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}
