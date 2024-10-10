using E_CommerceProject.Entities.Models;
using E_CommerceProject.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor) : Controller
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public async Task<IActionResult> UsersList()
        {
            var users = await _userManager.Users.ToListAsync();

            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName!,
                    Email = user.Email!,
                    Roles = roles
                });
            }

            return View(userViewModels);
        }

        public async Task<IActionResult> RolesList()
        {
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                RoleName = r.Name!
            }).ToListAsync();

            return View(roles);
        }

        public IActionResult CreateRole()
        {
            var roleViewModel = new RoleViewModel()
            {
                Id = Guid.NewGuid().ToString()
            };
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Id = model.Id,
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
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
                Roles = userRoles.Select(r => new RolesCheckedViewModel()
                {
                    RoleName = r.Name!,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name!).Result
                }).ToList()
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


        public IActionResult ChangePassword()
        {
            return View();
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string Username = _contextAccessor.HttpContext!.User.Identity!.Name!;
                var user = await _userManager.FindByNameAsync(Username);
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

}
