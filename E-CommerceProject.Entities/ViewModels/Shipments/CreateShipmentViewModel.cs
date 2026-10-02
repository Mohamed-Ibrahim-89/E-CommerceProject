namespace E_CommerceProject.Entities.ViewModels.Shipments;

public class CreateShipmentViewModel
{
    public string Carrier { get; set; } = string.Empty;

    public string TrackingNumber { get; set; } = string.Empty;

    public DateTime ShippingDate { get; set; }

    public DateTime EstimatedDeliveryDate { get; set; }

    public decimal ShippingCost { get; set; }

    public int OrderId { get; set; }
}
