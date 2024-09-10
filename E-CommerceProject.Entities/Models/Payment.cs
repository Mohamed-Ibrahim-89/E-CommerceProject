
using E_CommerceProject.Entities.Models;

namespace E_CommerceProject.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentAmount { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        //public int OrderId { get; set; }
        //public Order order { get; set; } = default!;
    }
}
