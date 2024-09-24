using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Entities.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        [MaxLength(50)]
        public string Carrieer { get; set; } = string.Empty;
        [MaxLength(50)]
        public string TrackingNumber { get; set; } = string.Empty;
        public DateTime ShippingDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal ShippingCost { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; } = default!;
    }
}
