using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Entities.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public string Carrieer { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public int Cost { get; set; }

        //public int OrderId { get; set; }
        //public Order order { get; set; } = default!;
    }
}
