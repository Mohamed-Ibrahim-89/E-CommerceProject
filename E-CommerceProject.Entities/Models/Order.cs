using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Entities.Models
{
    public class Order
    {
        public int Orderid { get; set; }
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Column(TypeName ="decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<OrderItem>? Items { get; set; }
    }
}
