namespace E_CommerceProject.Entities.Models;

public class Order : BaseEntity
{
    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    [Display(Name = "Order Date")]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Column(TypeName ="decimal(10,2)"), Display(Name = "Total Price")]
    public decimal TotalPrice { get; set; }

    public int CustomerInfoId { get; set; }

    public CustomerInfo? CustomerInfo { get; set; }

    public List<OrderDetail>? OrderDetails { get; set; }
}
