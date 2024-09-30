namespace E_CommerceProject.Entities.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int Amount { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public int CustomerInfoId { get; set; }
        public CustomerInfo? CustomerInfo { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}
