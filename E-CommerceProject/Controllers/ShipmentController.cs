using NuGet.Protocol.Core.Types;

namespace E_CommerceProject.Controllers;

[Authorize]
public class ShipmentController(IShipmentRepository repo,
    IToastNotification toastNotification,
    IHttpContextAccessor contextAccessor
    ) : BaseController(contextAccessor)
{
    private readonly IShipmentRepository _repo = repo;
    private readonly IToastNotification _toastNotification = toastNotification;

    public async Task<IActionResult> Index()
    {
        var userId = await GetSignedUserId();
        var shipments = await _repo.GetCustomerListAsync(userId, CancellationToken.None);
        return View(shipments);
    }
    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> List()
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
            var dtParams = GetDatatableParamsFromRequest();
            var jsonData = await _repo.GetAdminListAsync(dtParams, token);
            return JsonConvert.SerializeObject(jsonData);

        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return JsonConvert.SerializeObject(new { error = ex.Message });
        }
    }

    public async Task<IActionResult> Details(int shipmentId)
    {
        try
        {
            var shipmentDetails = await _repo.GetByIdAsync(shipmentId, CancellationToken.None);
            if (shipmentDetails == null)
                return NotFound();
            
            return View(shipmentDetails);

        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View("Error");
        }
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> Edit(int shipmentId, CancellationToken token)
    {
        try
        {
            var shipment = await _repo.GetForUpdate(shipmentId, token);
            if (shipment == null)
                return NotFound();
            
            return View(shipment);

        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateShipmentViewModel shipment, CancellationToken token)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _repo.Update(shipment, token);
                return RedirectToAction(nameof(List));
            }
            return View(shipment);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(shipment);
        }
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var shipment = await _repo.GetForUpdate(id, CancellationToken.None);

            if (shipment == null)
                return NotFound();

            await _repo.Delete(id, CancellationToken.None);
            return Ok();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }

}
