namespace E_CommerceProject.Entities.ViewModels.Categories;

public class CreateCategoryViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
