using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class DiscountController : Controller
    {
        // GET: HomeController1
        private IBaseRepository<Discount> _discount ;

        public DiscountController(IBaseRepository<Discount> discount)
        {
           _discount = discount;
        }

        public async Task<IActionResult> Index()
        {
            // Asynchronously retrieve all discounts from the repository
            var discounts =await _discount.GetAll();
            return View(discounts);
        }

        // GET: HomeController1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Asynchronously retrieve a discount by its ID
            var discount = await _discount.GetById(id);
            if (discount == null)
            {
                // Return a 404 Not Found response if the discount does not exist

                return NotFound();
            }
            

            return View(discount);
        }

        // GET:Discount/Create
        public ActionResult Create()
        {
            // Return the view for creating a new discount
            return View("NewDiscount");
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discount discount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Asynchronously add the new discount to the repository
                    var discountText = await _discount.AddItem(discount);

                    // Redirect to the Index action to show the updated list of discounts
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // If an error occurs, return the view to create a new discoun
                    return View("NewDiscount");
                }
            }
            // If the model is invalid, return the view with the current discount data
            return View("NewDiscount");
        }

        // GET: HomeController1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Asynchronously retrieve the discount to edit by its ID
            var discount = await _discount.GetById(id);
            if (discount == null)
            {
                return NotFound();
            }
            // Return the view with the discount data to edit
            return View(discount);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Discount discount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Asynchronously update the discount in the repository
                    await _discount.UpdateItem(discount);
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    return View(discount);
                }

            }
            return View(discount);
        }

        // GET: HomeController1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            // Asynchronously retrieve the discount to delete by its ID
            var discount= await _discount.GetById(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Asynchronously delete the discount from the repository

                await _discount.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
