using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace E_CommerceProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DiscountController(IBaseRepository<Discount> discount, IToastNotification toastNotification) : Controller
    {
        private readonly IBaseRepository<Discount> _discountRepository = discount;
        private readonly IToastNotification _toastNotification = toastNotification;

        public async Task<IActionResult> List()
        {
            var discounts = await _discountRepository.GetAll();
            return View(discounts);
        }

        public ActionResult Create()
        {
            var discount = new Discount();
            return View("DiscountForm", discount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discount discount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var discountText = await _discountRepository.AddItem(discount);

                    _toastNotification.AddSuccessToastMessage("Discount added successfully");
                    return RedirectToAction(nameof(List));
                }
                catch
                {
                    return View("DiscountForm", discount);
                }
            }
            return View("DiscountForm", discount);
        }

        public async Task<IActionResult> Edit(int discountId)
        {
            var discount = await _discountRepository.GetById(d => d.DiscountId == discountId);

            if (discount == null)
                return NotFound();

            return View("DiscountForm", discount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Discount discount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _discountRepository.UpdateItem(discount);

                    _toastNotification.AddSuccessToastMessage("Discount updated successfully");
                    return RedirectToAction(nameof(List));
                }
                catch
                {
                    return View("DiscountForm", discount);
                }

            }
            return View("DiscountForm", discount);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _discountRepository.DeleteItem(id);
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
