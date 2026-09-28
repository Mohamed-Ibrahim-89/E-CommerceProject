namespace E_CommerceProject.Entities.Models;

[Index(nameof(Status))]
[Index(nameof(OrderDate))]
public class Order : BaseEntity
{
    [MaxLength(50)]
    public OrderStatus Status { get; set; }

    public DateTime OrderDate { get; set; }

    [Column(TypeName ="decimal(10,2)")]
    public decimal TotalPrice { get; set; }

    public int CustomerInfoId { get; set; }
    public CustomerInfo? CustomerInfo { get; set; }

    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }

    public List<OrderDetail>? OrderDetails { get; set; }
}
