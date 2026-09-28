namespace E_CommerceProject.Controllers;

[Authorize(Roles = Constants.Roles.Admin)]
public class UserController(IUserRepository repository
    ,UserManager<User> userManager
    ,RoleManager<IdentityRole> roleManager
    ,IHttpContextAccessor contextAccessor
    ,IToastNotification toastNotification
    ) : BaseController(contextAccessor)
{
    private readonly IUserRepository _repository = repository;
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IToastNotification _toastNotification = toastNotification;


    public IActionResult Index()
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

    public async Task<string> GetUsersList(CancellationToken token)
    {
        try
        {
            var dtParams = GetDatatableParamsFromRequest();
            var jsonData = await _repository.GetList(dtParams, token);
            return JsonConvert.SerializeObject(jsonData);

        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return JsonConvert.SerializeObject(new { error = ex.Message });
        }
    }

    public async Task<IActionResult> Details(string userId, CancellationToken token)
    {
        try
        {
            var user = await _repository.GetById(userId, token);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View("Error");
        }
    }



    public async Task<IActionResult> ManageRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        var userRoles = await _roleManager.Roles.ToListAsync();

        var userRolesViewModel = new UserRolesViewModel()
        {
            UserId = user.Id,
            Username = user.UserName!,
            Email = user.Email!,
            Roles = [.. userRoles.Select(r => new RolesCheckedViewModel()
            {
                RoleName = r.Name!,
                IsSelected = _userManager.IsInRoleAsync(user, r.Name!).Result
            })]
        };

        return View(userRolesViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(UserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected == true).Select(rn => rn.RoleName));

        return RedirectToAction("UsersList");
    }

    [AllowAnonymous]
    public IActionResult ChangePassword()
    {
        return View();
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var UserId = await GetSignedUserId();
            var user = await _userManager.FindByIdAsync(UserId);
            var result = await _userManager.ChangePasswordAsync(user!, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("index", "home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }
}
