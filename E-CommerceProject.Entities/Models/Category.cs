namespace E_CommerceProject.Entities.Models;

[Index(nameof(Name), IsUnique = true)]
public class Category :  BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

}
