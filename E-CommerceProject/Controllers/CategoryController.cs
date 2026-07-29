namespace E_CommerceProject.Controllers;

[Authorize(Roles = Constants.Roles.Admin)]
public class CategoryController(ICategoryRepository  repository
                                ,IToastNotification toastNotification
                                ): BaseController
{
    private readonly ICategoryRepository _repository = repository;
    private readonly IToastNotification _toastNotification = toastNotification;

    public async Task<IActionResult> Index()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View();
        }
    }

    public async Task<string> GetCategoriesList(CancellationToken token)
    {
        try
        {
            var dtParams = GetDatatableParamsFromRequest();
            var jsonData = await _repository.GetListAsync(dtParams, token);
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
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCategoryViewModel model, CancellationToken token)
    {
        try
        {
            await _repository.AddAsync(model, token);

            _toastNotification.AddSuccessToastMessage("Category added successfully");
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex) 
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View(model);
        }
    }

    public async Task<ActionResult> Edit(int categoryId, CancellationToken token)
    {
        try
        {
            var model = await _repository.GetByIdAsync(categoryId, token);

            return View(new EditCategoryViewModel
            {
                CategoryId = categoryId,
                Name = model.Name
            });
        }
        catch(Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Edit(EditCategoryViewModel model, CancellationToken token)
    {
        try
        {
            await _repository.UpdateAsync(model, token);

            _toastNotification.AddSuccessToastMessage("Category updated successfully");
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> Delete(int categoryId, CancellationToken token)
    {
        try
        {
            await _repository.DeleteAsync(categoryId, token);
            _toastNotification.AddSuccessToastMessage("Category deleted successfully");
            return Ok();
        }
        catch (Exception ex)
        {
            _toastNotification?.AddErrorToastMessage(ex.Message);
            return View();
        }
    }
}
