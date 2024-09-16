namespace E_CommerceProject.Entities.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int Amount { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set;}

        public int CustomerId { get; set; }
        public CustomerInfo Customer { get; set; } = default!;
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
    }
}
