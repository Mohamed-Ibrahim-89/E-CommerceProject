namespace E_CommerceProject.Entities.Models;

public class Shipment : BaseEntity
{
    [MaxLength(50)]
    public string Carrier { get; set; } = string.Empty;
    [MaxLength(50), Display(Name = "Tracking Number")]
    public string TrackingNumber { get; set; } = string.Empty;
    [Display(Name = "Shipping Date")]
    public DateTime ShippingDate { get; set; }
    [Display(Name = "Estimated Delivery Date")]
    public DateTime EstimatedDeliveryDate { get; set; }
    [Column(TypeName = "decimal(6, 2)"), Display(Name = "Shipping Cost")]
    public decimal ShippingCost { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }
}
