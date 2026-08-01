namespace E_CommerceProject.Entities.Models;

public class Wishlist : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }


}
