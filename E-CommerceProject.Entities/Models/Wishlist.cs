namespace E_CommerceProject.Entities.Models
{
    public class Wishlist
    {

        public int WishlistId { get; set; }  
        public int CustomerInfoId { get; set; }
        public CustomerInfo? CustomerInfo { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }


    }
}
