
using E_CommerceProject.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Entities.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [MaxLength(30)]
        public string PaymentMethod { get; set; } = string.Empty;

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PaymentAmount { get; set; }

        public int CustomerInfoId { get; set; }
        public CustomerInfo? CustomerInfo { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
