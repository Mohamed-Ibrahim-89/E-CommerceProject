namespace E_CommerceProject.Entities.Models
{
    public class Wishlist
    {

        public int WishlistId { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }


    }
}
