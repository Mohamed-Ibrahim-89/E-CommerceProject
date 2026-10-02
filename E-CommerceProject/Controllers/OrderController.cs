using E_CommerceProject.Entities.ViewModels.Orders;

namespace E_CommerceProject.Controllers;

[Authorize]
public class OrderController(
    IOrderRepository repository,
    ICartRepository cartRepo,
    ICustomerInfoRepository CustomerRepo,
    IShipmentRepository shipmentRepo
    ,ICartRepository cartRepository
    ,IToastNotification toastNotification
    ,IHttpContextAccessor contextAccessor
    ) : BaseController(contextAccessor)
{
    private readonly IOrderRepository  _repository = repository;
    private readonly ICartRepository _cartRepo = cartRepo;
    private readonly ICustomerInfoRepository _customerRepo = CustomerRepo;
    private readonly IShipmentRepository _shipmentRepo = shipmentRepo;

    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IToastNotification _toastNotification = toastNotification;


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

    public async Task<ActionResult> Details(int orderId)
    {
        try
        {
            var orderDetails = await _repository.GetOrderDetailsAsync(orderId, CancellationToken.None);
            if (orderDetails == null)
                return NotFound();

            return View(orderDetails);

        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View("Error");
        }
    }

    public async Task<ActionResult> Checkout(CancellationToken token)
    {
        var userId = await GetSignedUserId();
        var cartItems = await _cartRepo.GetCartItems(userId, token);
        if (cartItems.Count == 0)
        {
            _toastNotification.AddErrorToastMessage("Your cart is empty, add some items first");
            return RedirectToAction(nameof(Index), "Cart");
        }
        else
        {
            if (userId != null)
            {
                var customerInfo = await _customerRepo.GetByIdAsync(userId, token);
                if(customerInfo == null)
                    return View(new CreateOrderViewModel());

                var viewModel = new CreateOrderViewModel
                {
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    DateOfBirth = customerInfo.DateOfBirth,
                    AddressLine1 = customerInfo.AddressLine1,
                    AddressLine2 = customerInfo.AddressLine2,
                    City = customerInfo.City,
                    State = customerInfo.State,
                    ZipCode = customerInfo.ZipCode,
                    Country = customerInfo.Country,
                    Landmark = customerInfo.Landmark,
                    PhoneNumber = customerInfo.PhoneNumber
                };
                return View(viewModel);
            }

            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Checkout(CreateOrderViewModel model, CancellationToken token)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var userId = await GetSignedUserId();

                var orderId = await _repository.CreateOrderAsync(model, userId, token);
                await _cartRepository.ClearCart(userId, token);
                await _shipmentRepo.CreateAsync(new CreateShipmentViewModel
                {
                    OrderId = orderId,
                    ShippingDate = DateTime.Now,
                    EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(1, 5)),
                    Carrier = "Default",
                    TrackingNumber = "01000050050",
                    ShippingCost = new Random().Next(40,100)
                }, token);

                _toastNotification.AddSuccessToastMessage("Thanks for your order. You'll get it soon");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View();
            }
        }
        return View();
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repository.DeleteAsync(id, CancellationToken.None);
            return Ok();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }

}
