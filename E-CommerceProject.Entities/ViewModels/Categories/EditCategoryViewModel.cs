namespace E_CommerceProject.Entities.ViewModels.Categories;

public class EditCategoryViewModel
{
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}
