namespace E_CommerceProject.Controllers;

[Authorize(Roles = Constants.Roles.Admin)]
public class CategoryController(IBaseRepository<Category> categoryRepository
                                ,ICategoryRepository  repository
                                ,IToastNotification toastNotification
                                ): BaseController
{
    private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;
    private readonly ICategoryRepository _repository = repository;
    private readonly IToastNotification _toastNotification = toastNotification;

    public async Task<ActionResult> List()
    {
        var categories = await _categoryRepository.GetAll();
        return View(categories);
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View("Error");
        }
    }

    public async Task<string> GetCategoriesList()
    {
        try
        {
            var dtParams = GetDatatableParamsFromRequest();
            var jsonData = await _repository.GetCategoriesList(dtParams);
            return JsonConvert.SerializeObject(jsonData);
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return JsonConvert.SerializeObject(new { error = ex.Message });
        }
    }

    public ActionResult Create()
    {
        var categories = new Category();
        return View("CategoryForm", categories);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Category item)
    {
        try
        {
            var categoryTest = _categoryRepository.GetAll().Result.Any
                (c => c.Name == item.Name);
            if (categoryTest)
            {
                ViewBag.ExistsError = "Category Name already exists";
                return View("CategoryForm", item);
            }
            await _categoryRepository.AddItem(item);

            _toastNotification.AddSuccessToastMessage("Category added successfully");
            return RedirectToAction(nameof(List));
        }
        catch
        {
            return View("CategoryForm", item);
        }
    }

    public async Task<ActionResult> Edit(int categoryId)
    {
        var category = await _categoryRepository.GetById(c => c.CategoryId == categoryId);
        if (category == null)
        {
            return RedirectToAction(nameof(List));
        }
        return View("CategoryForm", category);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(Category Item)
    {
        try
        {
            await _categoryRepository.UpdateItem(Item);

            _toastNotification.AddSuccessToastMessage("Category updated successfully");
            return RedirectToAction(nameof(List));
        }
        catch
        {
            return View("CategoryForm");
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoryRepository.DeleteItem(id);
            return Ok();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }
}
