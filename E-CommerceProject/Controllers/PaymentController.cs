using Microsoft.AspNetCore.Mvc;
using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;

namespace E_CommerceProject.Controllers
{
    public class PaymentController(IBaseRepository<Payment> paymentRepository) : Controller
    {
        private readonly IBaseRepository<Payment> _paymentRepository = paymentRepository;

        public async Task<ActionResult> List()
        {
            var payments = await _paymentRepository.GetAll();
            return View(payments);
        }

        public async Task<ActionResult> Details(int id)
        {
            var payment = await _paymentRepository.GetById(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Payment payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _paymentRepository.AddItem(payment);
                    return RedirectToAction(nameof(List));
                }
                return View(payment);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var payment = await _paymentRepository.GetById(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Payment payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _paymentRepository.UpdateItem(payment);
                    return RedirectToAction(nameof(List));
                }
                return View(payment);
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var shipment = await _paymentRepository.GetById(s => s.PaymentId == id);

                if (shipment == null)
                {
                    return NotFound();
                }

                await _paymentRepository.DeleteItem(id);
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
