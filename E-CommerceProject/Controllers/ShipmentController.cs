using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    [Authorize]
    public class ShipmentController(IBaseRepository<Shipment> shipmentRepository , IBaseRepository<Order> orderRepository , IHttpContextAccessor contextAccessor , UserManager<AppUser> userManager) : Controller
    {
        private readonly IBaseRepository<Shipment> _shipmentRepository = shipmentRepository;
        private readonly IBaseRepository<Order> _orderRepository = orderRepository;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        public async Task<IActionResult> Index()
        {
            var userId = await GetSignedUserId();
            var shipments = await _shipmentRepository.GetAll(s => s.Order!.CustomerInfo!.AppUserId == userId , ["Order"]);
            return View(shipments);
        }

        public async Task<IActionResult> List()
        {
            var shipments = await _shipmentRepository.GetAll(null, new[] { "Order" });
            return View(shipments);
        }
        public async Task<IActionResult> Details(int shipmentId)
        {
            var shipment = await _shipmentRepository.GetById(s => (s.ShipmentId == shipmentId));
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        public async void Create(Shipment shipment)
        {
            try
            {
                await _shipmentRepository.AddItem(shipment);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
        }

        public async Task<IActionResult> Edit(int shipmentId)
        {
            var shipment = await _shipmentRepository.GetById(s => s.ShipmentId == shipmentId);
            if (shipment == null)
            {
                return NotFound();
            }

            return View("Edit", shipment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Shipment shipment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _shipmentRepository.UpdateItem(shipment);
                    return RedirectToAction("List");
                }
                return View("Edit", shipment);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", shipment);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var shipment = await _shipmentRepository.GetById(s => s.ShipmentId == id);

                if (shipment == null)
                {
                    return NotFound();
                }

                await _shipmentRepository.DeleteItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        public async Task<string> GetSignedUserId()
        {
            var username = _contextAccessor!.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(username!);

            return user!.Id;
        }
    }
}
