namespace E_CommerceProject.Components;

public class CategoryMenu(IBaseRepository<Category> categoryRepository) : ViewComponent
{
    private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _categoryRepository.GetAll();
        var orderedCategories = categories.OrderBy(c => c.Name);

        return View(orderedCategories);
    }
}
