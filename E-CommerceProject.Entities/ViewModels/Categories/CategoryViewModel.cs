namespace E_CommerceProject.Entities.ViewModels.Categories;

public class CategoryViewModel
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }
}
