using E_CommerceProject.Entities.Models;
using E_CommerceProject.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);

        }

        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(model.Username);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
                    {
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("List", "Product");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("List", "Product");
                        }
                        return Redirect(returnUrl);
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> R(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> L(AccountViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(model.Username);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View("Index", model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
                    {
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("List", "Product");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("List", "Product");
                        }
                        return Redirect(returnUrl);
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index", model);
        }
    }
}
