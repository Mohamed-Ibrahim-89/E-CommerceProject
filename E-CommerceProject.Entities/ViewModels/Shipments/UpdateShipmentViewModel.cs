namespace E_CommerceProject.Entities.ViewModels.Shipments;

public class UpdateShipmentViewModel
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
}
