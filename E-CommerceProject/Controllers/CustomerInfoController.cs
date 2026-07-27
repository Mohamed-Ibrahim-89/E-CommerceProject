namespace E_CommerceProject.Controllers;

public class CustomerInfoController(IBaseRepository<CustomerInfo> customerInfoRepository, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor) : Controller
{
    private readonly IBaseRepository<CustomerInfo> _customerInfoRepository = customerInfoRepository;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public async Task<IActionResult> Index()
    {
        var appUserId = await GetSignedUserId();
        var customerInfo = await _customerInfoRepository.GetById(ci => ci.AppUserId == appUserId);

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
    public async Task<IActionResult> Create(CustomerInfo model)
    {
        if (ModelState.IsValid)
        {
            var appUser = await _userManager.GetUserAsync(User);
            model.AppUserId = appUser.Id;

            await _customerInfoRepository.AddItem(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit()
    {
        var appUserId = await GetSignedUserId();
        var customerInfo = await _customerInfoRepository.GetById(ci => ci.AppUserId == appUserId);

        if (customerInfo == null)
        {
            return NotFound();
        }

        return View(customerInfo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CustomerInfo model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _customerInfoRepository.UpdateItem(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<string> GetSignedUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User not found");
        return userId;
    }
}
