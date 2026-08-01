namespace E_CommerceProject.Controllers;

public class HomeController(IProductRepository repository
    ,ICategoryRepository categoryRepository
    ,IToastNotification toastNotification
    ,IHttpContextAccessor contextAccessor) : BaseController(contextAccessor)
{
    private readonly IProductRepository _repository = repository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IToastNotification _toastNotification = toastNotification;

    public async Task<IActionResult> Index()
    {
        try
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetCategoryAsDropDown(), "Id", "Name");
            return View();
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View("Error");
        }
    }

    public async Task<string> GetList(CancellationToken token)
    {
        try
        {
            var dtParams = GetDatatableParamsFromRequest();
            var jsonData = await _repository.GetListForHomePageAsync(dtParams, token);
            return JsonConvert.SerializeObject(jsonData);

        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return JsonConvert.SerializeObject(new { error = ex.Message });
        }
    }
}
