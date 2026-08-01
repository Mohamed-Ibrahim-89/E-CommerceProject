namespace E_CommerceProject.Entities.Models;

[Index(nameof(CreatedAt))]
[Index(nameof(UpdatedAt))]
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
