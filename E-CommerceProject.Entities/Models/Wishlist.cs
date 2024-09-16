namespace E_CommerceProject.Entities.Models
{
    public class Wishlist
    {

        public int WishlistId { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }

        public int CustomerId { get; set; }
        public CustomerInfo? Customer { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }


    }
}
