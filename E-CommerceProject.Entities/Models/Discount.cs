namespace E_CommerceProject.Entities.Models;

[Index(nameof(Name))]
public class Discount : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal Percentage { get; set; }

    public bool Active { get; set; }
}
