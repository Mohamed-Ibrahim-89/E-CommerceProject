using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Implementations;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace E_CommerceProject.Controllers
{
    public class OrderController(IBaseRepository<Order> orderRepository, ICartRepository cartRepository, IToastNotification toastNotification) : Controller
    {
        private readonly IBaseRepository<Order> _orderRepository = orderRepository;
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IToastNotification _toastNotification = toastNotification;

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> List()
        {
            var orders = await _orderRepository.GetAll(null, ["CustomerInfo"]);
            return View(orders);
        }

        public async Task<ActionResult> Details(int orderId)
        {
            var order = await _orderRepository.GetById(o => o.OrderId == orderId, ["CustomerInfo", "OrderDetails", "OrderDetails.Product"]);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public async Task<ActionResult> Checkout()
        {
            var cartItems = await _cartRepository.GetCartItems();
            if (cartItems.Count == 0)
            {
                _toastNotification.AddErrorToastMessage("Your cart is empty, add some pies first");
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                var order = new Order();
                return View("OrderForm", order);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(Order order)
        {
            var cartItems = await _cartRepository.GetCartItems();

            if (ModelState.IsValid)
            {
                try
                {
                    order.TotalPrice = cartItems.Sum(c => c.Product!.Price * c.Amount);
                    order.OrderDetails = [];
                    foreach (var item in cartItems)
                    {
                        order.OrderDetails.Add(new OrderDetail
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Amount,
                            Price = item.Product!.Price
                        });
                    }

                    await _orderRepository.AddItem(order);
                    await _cartRepository.ClearCart();

                    _toastNotification.AddSuccessToastMessage("Thanks for your order. You'll get it soon");
                    return RedirectToAction("Index", "Home");
                }
                catch(Exception ex)
                {
                    _toastNotification.AddErrorToastMessage(ex.Message);
                    return View("OrderForm");
                }
            }
            return View("OrderForm");
        }

        public async Task<ActionResult> Edit(int orderId)
        {
            var order = await _orderRepository.GetById(o => o.OrderId == orderId, ["CustomerInfo"]);
            if (order == null)
            {
                return NotFound();
            }
            return View("OrderForm", order);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Order order)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _orderRepository.UpdateItem(order);
                }
                catch
                {
                    return View("OrderForm", order);
                }
                return RedirectToAction(nameof(List));
            }
            return View("OrderForm", order);
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderRepository.DeleteItem(id);
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
