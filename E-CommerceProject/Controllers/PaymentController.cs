using Microsoft.AspNetCore.Mvc;
using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using System.Threading.Tasks;

namespace E_CommerceProject.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IBaseRepository<Payment> _paymentRepository;

        public PaymentController(IBaseRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        // GET: PaymentController/List
        public async Task<ActionResult> List()
        {
            var payments = await _paymentRepository.GetAll();
            return View(payments);
        }

        // GET: PaymentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var payment = await _paymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // GET: PaymentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentController/Create
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

        // GET: PaymentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var payment = await _paymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: PaymentController/Edit/5
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

        // GET: PaymentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var payment = await _paymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: PaymentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _paymentRepository.DeleteItem(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}
