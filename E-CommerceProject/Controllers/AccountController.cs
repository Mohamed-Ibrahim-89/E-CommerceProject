namespace E_CommerceProject.Controllers;

public class AccountController(
    IAccountRepository repository
    ,IToastNotification toastNotification
    ,IHttpContextAccessor contextAccessor
    ) : BaseController(contextAccessor)
{
    private readonly IAccountRepository _repository = repository;
    private readonly IToastNotification _toastNotification = toastNotification;

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken token)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _repository.RegisterAsync(model, token);
                _toastNotification.AddSuccessToastMessage("Registration successful, you can now log in.");
                return RedirectToAction(nameof(Login));
            }
            else 
            {
                _toastNotification.AddErrorToastMessage("Please check your inputs.");
                return View(model);
            }
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View(model);
        }
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null, CancellationToken token = default)
    {
        try 
        { 
            returnUrl ??= Url.Action("Index", "Home");

            if (ModelState.IsValid)
            {
                await _repository.LoginAsync(model, token);
                return LocalRedirect(returnUrl!);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(Login), model);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(nameof(Login), model);
        }
    }
    [Authorize]
    public async Task<IActionResult> Profile(CancellationToken token)
    {
        try
        {
            var userId = await GetSignedUserId();
            var user = await _repository.GetUserProfileAsync(userId, token);
            return View(user);
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _repository.LogoutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
