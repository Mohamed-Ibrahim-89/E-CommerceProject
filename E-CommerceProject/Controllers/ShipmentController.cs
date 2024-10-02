using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class ShipmentController(IBaseRepository<Shipment> shipmentRepository , IBaseRepository<Order> orderRepository) : Controller
    {
        private readonly IBaseRepository<Shipment> _shipmentRepository = shipmentRepository;
        private readonly IBaseRepository<Order> _orderRepository = orderRepository;
        public async Task<IActionResult> List()
        {
            var shipments = await _shipmentRepository.GetAll(null, ["Order"]);
            return View(shipments);
        }

        public async Task<IActionResult> Details(int shipmentId)
        {
            var shipment = await _shipmentRepository.GetById(s => s.ShipmentId == shipmentId);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        public IActionResult Create()
        {
            var shipment = new Shipment
            {
                ShippingDate = DateTime.Now,
                EstimatedDeliveryDate = DateTime.Now.AddDays(2)
            };
            return View("ShipmentForm", shipment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shipment shipment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _shipmentRepository.AddItem(shipment);
                    return RedirectToAction("List");
                }
                return View("ShipmentForm", shipment);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("ShipmentForm", shipment);
            }
        }

        public async Task<IActionResult> Edit(int shipmentId)
        {
            var shipment = await _shipmentRepository.GetById(s => s.ShipmentId == shipmentId);
            if (shipment == null)
            {
                return NotFound();
            }

            return View("ShipmentForm", shipment);
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
                return View("ShipmentForm", shipment);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("ShipmentForm", shipment);
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

        public async Task<IActionResult> Search(string searchQuery)
        {
            IEnumerable<Shipment> shipments;

            if (searchQuery != null)
            {
                ViewBag.SearchQuery = searchQuery;
                shipments = await _shipmentRepository.GetAll(s => s.TrackingNumber.Contains(searchQuery), new[] { "Order" });
            }
            else
            {
                shipments = await _shipmentRepository.GetAll(null, new[] { "Order" });
            }

            return PartialView("_ShipmentCard", shipments);
        }
    }
}
