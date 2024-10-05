using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Implementations;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class OrderController : Controller
    {
        private IBaseRepository<Order> _order;
        private readonly ICartRepository _cartRepository;
        public OrderController(IBaseRepository<Order> order, ICartRepository cartRepository)
        {
            _order = order;
            _cartRepository = cartRepository;
        }

        public async Task< ActionResult> Index()
        {
            var orders =await _order.GetAll();
            return View(orders);
        }

        public async Task< ActionResult >Details(int id)
        {
            var order =await _order.GetById(o => o.Orderid == id);
            if (order == null)
            { 
                   return NotFound();
            }
            return View(order);
        }
        public ActionResult Checkout()
        {
            var cart = _cartRepository.GetCartItems().Result.Last();
            var order = new Order
            {
                CartId = cart.CartId
            };
            return View("OrderForm",order);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Checkout(Order order)
        {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var orderCreate =await _order.AddItem(order);
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View("OrderForm");
                    }
                }
                return View("OrderForm");
        }

        public async Task< ActionResult> Edit(int id)
        {
            var order= await _order.GetById(o => o.Orderid == id);
            if (order==null) 
            {
                return NotFound();
            }
            return View("OrderForm", order);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _order.UpdateItem(order);
                }
                catch
                {
                    return View("OrderForm", order);
                }
                return RedirectToAction(nameof(Index));
            }
            return View("OrderForm", order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _order.GetById(o => o.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id )
        {
            try
            {
               

                await _order.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
             
                    
    }
}
