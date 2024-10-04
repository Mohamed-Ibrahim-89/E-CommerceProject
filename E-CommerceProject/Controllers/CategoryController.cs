using Microsoft.AspNetCore.Mvc;
using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController(IBaseRepository<Category> categoryRepository, IToastNotification toastNotification) : Controller
    {
        private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;
        private readonly IToastNotification _toastNotification = toastNotification;

        public async Task<ActionResult> List()
        {
            var categories = await _categoryRepository.GetAll();
            return View(categories);
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
}
