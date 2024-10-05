using E_CommerceProject.Entities.Models;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        private IBaseRepository<Order> _order;
        public OrderController(IBaseRepository<Order> order)
        {
            _order = order;
        }

        public async Task< ActionResult> Index()
        {
            var orders =await _order.GetAll();
            return View(orders);
        }

        // GET: OrderController/Details/5
        public async Task< ActionResult >Details(int id)
        {
            var order =await _order.GetById(o => o.Orderid == id);
            if (order == null)
            { 
                   return NotFound();
            }
            return View(order);
        }

        // GET: OrderController/Create
        public ActionResult Create(int cartId)
        {

            var order=new Order();
            return View("OrderForm",order);
           
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(Order order)
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

        // GET: OrderController/Edit/5
        public async Task< ActionResult> Edit(int id)
        {
            var order= await _order.GetById(o => o.Orderid == id);
            if (order==null) 
            {
                return NotFound();
            }
            return View("OrderForm", order);
            
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _order.GetById(o => o.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Delete/5
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
