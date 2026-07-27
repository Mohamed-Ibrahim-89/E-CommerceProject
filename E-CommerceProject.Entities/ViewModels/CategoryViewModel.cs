namespace E_CommerceProject.Entities.ViewModels;

public class CategoryViewModel
{
    public int CategoryId { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }
}
