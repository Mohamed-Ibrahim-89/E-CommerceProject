namespace E_CommerceProject.Controllers;

[Authorize(Roles = Constants.Roles.Admin)]
public class ProductController(IProductRepository productRepository
    , ICategoryRepository categoryRepository
    , IDiscountRepository discountRepository
    , IToastNotification toastNotification
    ) : BaseController
{
    private readonly IProductRepository  _repository         = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IDiscountRepository _discountRepository = discountRepository;
    private readonly IToastNotification  _toastNotification  = toastNotification;

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
    public async Task<string> GetProductsList(CancellationToken token)
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

    [AllowAnonymous]
    public async Task<IActionResult> Details(int productId, CancellationToken token)
    {
        try
        {
            var viewModel = await _repository.GetByIdAsync(productId, token);

            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }
        catch (InvalidOperationException ex)
        {
            ViewData["Error"] = ex.Message;
        }

        return View();
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            await LoadViewBags();
            return View();
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductViewModel model, CancellationToken token)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(model, token);
                _toastNotification.AddSuccessToastMessage("Item added successfully");
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View(model);
        }
    }


    public async Task<IActionResult> Edit(int productId)
    {
        try
        {
            var viewModel = await _repository.GetEntity(productId);
            if (viewModel == null)
                return NotFound();

            await LoadViewBags();

            return View(new EditProductViewModel
            {
                ProductId = viewModel.ProductId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Cover = viewModel.Cover,
                QuantityInStock = viewModel.QuantityInStock,
                CategoryId = viewModel.CategoryId,
                DiscountId = viewModel.DiscountId,
            });
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProductViewModel model, CancellationToken token)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(model, token);
                _toastNotification.AddSuccessToastMessage("Item updated successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View(model);
        }
    }

    public async Task<IActionResult> Delete(int id, CancellationToken token)
    {
        try
        {
            await _repository.DeleteAsync(id, token);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View();
        }
    }

    public async Task LoadViewBags()
    {
        ViewBag.Categories = new SelectList(await _categoryRepository.GetCategoryAsDropDown(), "Id", "Name");
        ViewBag.Discounts = new SelectList(await _discountRepository.GetDiscountsAsDropDown(), "Id", "Name");
    }
}
