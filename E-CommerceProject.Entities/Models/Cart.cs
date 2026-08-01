namespace E_CommerceProject.Entities.Models;

public class Cart : BaseEntity
{
    public int Amount { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}
