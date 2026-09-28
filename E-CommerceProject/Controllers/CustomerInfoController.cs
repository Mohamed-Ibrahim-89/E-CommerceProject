namespace E_CommerceProject.Controllers;

public class CustomerInfoController(ICustomerInfoRepository repository
    , IBaseRepository<CustomerInfo> customerInfoRepository
    , UserManager<User> userManager
    , IHttpContextAccessor contextAccessor
    ) : BaseController(contextAccessor)
{
    private readonly ICustomerInfoRepository _repository = repository;
    private readonly IBaseRepository<CustomerInfo> _customerInfoRepository = customerInfoRepository;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var appUserId = await GetSignedUserId();
        var customerInfo = await _repository.GetByIdAsync(appUserId, token);

        if(customerInfo == null)
        {
            return RedirectToAction(nameof(Create));
        }
        
        return View(customerInfo);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerInfoViewModel model, CancellationToken token)
    {
        if (ModelState.IsValid)
        {
            var userId = await GetSignedUserId();

            await _repository.AddAsync(model, userId, token);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userId = await GetSignedUserId();
        var customerInfo = await _repository.GetByIdAsync(userId, CancellationToken.None);

        if (customerInfo == null)
        {
            return NotFound();
        }

        return View(customerInfo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CustomerInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                //await _customerInfoRepository.UpdateItem(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
