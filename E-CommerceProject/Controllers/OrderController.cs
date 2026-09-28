namespace E_CommerceProject.Controllers;

[Authorize]
public class OrderController(IOrderRepository repository
    , IBaseRepository<Order> orderRepository
    ,ICartRepository cartRepository
    ,IToastNotification toastNotification
    ,IBaseRepository<OrderDetail> orderDetailRepository
    ,IBaseRepository<CustomerInfo> customerInfoRepository
    ,IHttpContextAccessor contextAccessor
    ,UserManager<User> userManager
    ,IBaseRepository<Shipment> shipmentRepository
    ) : BaseController(contextAccessor)
{
    private readonly IOrderRepository  _repository = repository;
    private readonly IBaseRepository<Order> _orderRepository = orderRepository;
    private readonly IBaseRepository<Shipment> _shipmentRepository = shipmentRepository;
    private readonly IBaseRepository<OrderDetail> _orderDetailRepository = orderDetailRepository;
    private readonly IBaseRepository<CustomerInfo> _customerInfoRepository = customerInfoRepository;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IToastNotification _toastNotification = toastNotification;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    [Authorize(Roles = Constants.Roles.Admin)]
    public IActionResult  Index()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View("Error");
        }
    }

    public async Task<string> GetList(CancellationToken token)
    {
        try
        {
            var dataTableParams = GetDatatableParamsFromRequest();
            var result = await _repository.GetOrderListAsync(dataTableParams, token);
            return JsonConvert.SerializeObject(result);
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return JsonConvert.SerializeObject(new { error = ex.Message });
        }
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<ActionResult> List()
    {
        var orders = await _orderRepository.GetAll(null, ["CustomerInfo"]);
        return View(orders);
    }

    public async Task<ActionResult> Details(int orderId)
    {
        var order = await _orderRepository.GetById(o => o.Id == orderId, ["CustomerInfo", "OrderDetails", "OrderDetails.Product", "OrderDetails.Product.Discount"]);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    public async Task<ActionResult> Checkout(CancellationToken token)
    {
        var userId = await GetSignedUserId();
        var cartItems = await _cartRepository.GetCartItems(userId, token);
        if (cartItems.Count == 0)
        {
            _toastNotification.AddErrorToastMessage("Your cart is empty, add some items first");
            return RedirectToAction("Index", "Cart");
        }
        else
        {
            if (userId != null)
            {
                var customerInfo = await _customerInfoRepository.GetById(ci => ci.UserId == userId);
                var order = new Order
                {
                    CustomerInfo = customerInfo,
                };
                return View("OrderForm", order);
            }

            return View("OrderForm", new Order());
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Checkout(Order order, CancellationToken token)
    {
        var userId = await GetSignedUserId();
        var cartItems = await _cartRepository.GetCartItems(userId, token);

        if (ModelState.IsValid)
        {
            try
            {
                order.TotalPrice = cartItems.Sum(c => (c.ProductPrice - (c.ProductPrice *(c.ProductDiscount / 100))) * c.Amount);
                order.OrderDetails = [];
                foreach (var item in cartItems)
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Amount,
                        Price = item.ProductPrice
                    });
                }

                if (userId != null)
                {
                    order.CustomerInfo!.UserId = userId;

                    await _orderRepository.AddItem(order);
                    await _cartRepository.ClearCart(userId, token);
                }

                var shipment = new Shipment
                {
                    OrderId = order.Id,
                    ShippingDate = DateTime.Now,
                    EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(1, 5)),
                    Carrieer = "Default",
                    TrackingNumber = "01000050050",
                    ShippingCost = new Random().Next(40,100)
                };
                await _shipmentRepository.AddItem(shipment);

                _toastNotification.AddSuccessToastMessage("Thanks for your order. You'll get it soon");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View("OrderForm");
            }
        }
        return View("OrderForm");
    }

    public async Task<ActionResult> Edit(int orderId)
    {
        var order = await _orderRepository.GetById(o => o.Id == orderId, ["CustomerInfo", "CustomerInfo.User"]);
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

    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {

            var orderDetailId = await _orderDetailRepository.GetById(od => od.OrderId == id);
            await _orderDetailRepository.DeleteItem(orderDetailId.Id);

            var order = await _orderRepository.GetById(c => c.Id == id);
            await _orderRepository.DeleteItem(id);

            await _customerInfoRepository.DeleteItem(order.CustomerInfoId);

            return Ok();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }

}
