﻿using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class ShipmentController(IBaseRepository<Shipment> shipmentRepository , IBaseRepository<Order> orderRepository , IHttpContextAccessor contextAccessor , UserManager<AppUser> userManager) : Controller
    {
        private readonly IBaseRepository<Shipment> _shipmentRepository = shipmentRepository;
        private readonly IBaseRepository<Order> _orderRepository = orderRepository;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        public async Task<IActionResult> index()
        {
            var userId = await GetSignedUserId();
            var shipments = await _shipmentRepository.GetAll(s => s.Order.CustomerInfo.AppUserId == userId , ["Order"]);
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
        public async Task<string> GetSignedUserId()
        {
            var username = _contextAccessor!.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(username!);

            return user!.Id;
        }
    }
}
