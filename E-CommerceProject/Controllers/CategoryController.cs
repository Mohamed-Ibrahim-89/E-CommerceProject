using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;

namespace SunPiontOfSaleFinalProject.App.Controllers
{
    public class CategoryController : Controller
    {
        private IBaseRepository<Category> _categoryRepository;

        public CategoryController(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        public async Task<ActionResult> List()
        {
            var categories = await _categoryRepository.GetAll();
            return View(categories);
        }

        public async Task<ActionResult> Details(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            return category != null ? View(category) : NotFound();
        }
        public ActionResult Create()
        {
            return View();
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
                    return View();
                }
                await _categoryRepository.AddItem(item);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return RedirectToAction(nameof(List));
            }
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Category Item)
        {
            try
            {
                await _categoryRepository.UpdateItem(Item);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var category = await _categoryRepository.GetById(id!.Value);
            if (category == null)
            {
                return RedirectToAction(nameof(List));
            }
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryRepository.DeleteItem(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}
