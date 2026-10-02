using E_CommerceProject.Entities.Constants;
using E_CommerceProject.Entities.ViewModels.Shipments;

namespace E_CommerceProject.Repositories.Repositories;

public interface IShipmentRepository
{
    Task<DatatableResult> GetAdminListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token);
    Task<List<ShipmentViewModel>> GetCustomerListAsync(string customerId, CancellationToken token);
    Task<ShipmentDetailsViewModel> GetByIdAsync(int shipmentId, CancellationToken token);
    Task<UpdateShipmentViewModel> GetForUpdate(int shipmentId, CancellationToken token);
    Task CreateAsync(CreateShipmentViewModel model, CancellationToken token);
    Task Update(UpdateShipmentViewModel model, CancellationToken token);
    Task Delete(int shipmentId, CancellationToken token);
}

public class ShipmentRepository(AppDbContext context) : IShipmentRepository
{
    private readonly AppDbContext _context = context;

    public async Task<DatatableResult> GetAdminListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token)
    {
        var cols = new Dictionary<string, Expression<Func<Shipment, object>>>
        {
            { nameof(AdminShipmentViewModel.Id), model => model.Id},
            { nameof(AdminShipmentViewModel.Carrier), model => model.Carrier },
            { nameof(AdminShipmentViewModel.TrackingNumber), model => model.TrackingNumber },
            { nameof(AdminShipmentViewModel.ShippingDate), model => model.ShippingDate },
            { nameof(AdminShipmentViewModel.EstimatedDeliveryDate), model => model.EstimatedDeliveryDate },
            { nameof(AdminShipmentViewModel.ShippingCost), model => model.ShippingCost },
            { nameof(AdminShipmentViewModel.Status), model => model.Order!.Status },
        };

        var lst = _context.Shipments
            .Include(a => a.Order)
            .AsNoTracking()
            .AsQueryable();

        if (dataTableParams.SortColumn != null)
        {
            var orderField = cols.ElementAt(int.Parse(dataTableParams.SortColumn));
            lst = dataTableParams.SortColumnDirection == "asc" ? lst.OrderBy(orderField.Value) : lst.OrderByDescending(orderField.Value);
        }
        else
        {
            lst = lst.OrderByDescending(a => a.Id);
        }

        if (!string.IsNullOrEmpty(dataTableParams.SearchValue))
        {
            Enum.TryParse<OrderStatus>(dataTableParams.SearchValue, true, out var status);

            lst = lst.Where(m => m.Carrier.Contains(dataTableParams.SearchValue)
                || m.TrackingNumber.Contains(dataTableParams.SearchValue)
                || m.ShippingCost.ToString().Contains(dataTableParams.SearchValue)
                || m.Order!.Status == status
                || m.Order!.CustomerInfo!.FirstName.Contains(dataTableParams.SearchValue)
                || m.Order!.CustomerInfo!.LastName.Contains(dataTableParams.SearchValue)
                || m.Order!.CustomerInfo!.PhoneNumber.Contains(dataTableParams.SearchValue)
            );
        }

        var count = await lst.CountAsync();

        var data = await lst
            .Select(a => new AdminShipmentViewModel
            {
                Id = a.Id,
                Carrier = a.Carrier,
                TrackingNumber = a.TrackingNumber,
                ShippingDate = a.ShippingDate,
                EstimatedDeliveryDate = a.EstimatedDeliveryDate,
                ShippingCost = a.ShippingCost,
                Status = a.Order!.Status,
                CustomerName = a.Order!.CustomerInfo!.FirstName + " " + a.Order!.CustomerInfo!.LastName,
                PhoneNumber = a.Order!.CustomerInfo!.PhoneNumber
            })
            .Skip(dataTableParams.Skip)
            .Take(dataTableParams.PageSize)
            .ToListAsync(token);

        var oResult = new DatatableResult
        {
            draw = dataTableParams.Draw
        };

        foreach (var item in data)
            oResult.data.Add(item);

        oResult.recordsTotal = count;
        oResult.recordsFiltered = count;
        return oResult;
    }

    public Task<List<ShipmentViewModel>> GetCustomerListAsync(string customerId, CancellationToken token)
    {
        var shipments = _context.Shipments
            .Where(s => s.Order!.CustomerInfo!.UserId == customerId)
            .Select(s => new ShipmentViewModel
            {
                Id = s.Id,
                Carrier = s.Carrier,
                TrackingNumber = s.TrackingNumber,
                ShippingDate = s.ShippingDate,
                EstimatedDeliveryDate = s.EstimatedDeliveryDate,
                ShippingCost = s.ShippingCost,
                Status = s.Order!.Status,
            })
            .ToListAsync(token);
        return shipments;
    }

    public async Task<ShipmentDetailsViewModel> GetByIdAsync(int shipmentId, CancellationToken token)
    {
        var shipment = await _context.Shipments
            .Where(s => s.Id == shipmentId)
            .Select(s => new ShipmentDetailsViewModel
            {
                // Shipment Information
                Carrier = s.Carrier,
                TrackingNumber = s.TrackingNumber,
                ShippingDate = s.ShippingDate,
                EstimatedDeliveryDate = s.EstimatedDeliveryDate,
                ShippingCost = s.ShippingCost,
                // Customer Information
                CustomerName = s.Order!.CustomerInfo!.FirstName + " " + s.Order!.CustomerInfo!.LastName,
                PhoneNumber = s.Order!.CustomerInfo!.PhoneNumber,
                AddressLine1 = s.Order!.CustomerInfo!.AddressLine1,
                AddressLine2 = s.Order!.CustomerInfo!.AddressLine2,
                // Order Information
                Status = s.Order!.Status,
                OrderDate = s.Order!.OrderDate,
                TotalPrice = s.Order!.TotalPrice,
                // Order Details
                OrderDetails = s.Order!.OrderDetails!.Select(od => new OrderDetailViewModel
                {
                    ProductName = od.Product!.Name,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            }
            ).FirstOrDefaultAsync(token);

        return shipment ?? throw new InvalidOperationException("Shipment not found");
    }

    public async Task<UpdateShipmentViewModel> GetForUpdate(int shipmentId, CancellationToken token)
    {
        var shipment = await _context.Shipments
            .Where(s => s.Id == shipmentId)
            .Select(s => new UpdateShipmentViewModel
            {
                Id = s.Id,
                Carrier = s.Carrier,
                TrackingNumber = s.TrackingNumber,
                ShippingDate = s.ShippingDate,
                EstimatedDeliveryDate = s.EstimatedDeliveryDate,
                ShippingCost = s.ShippingCost,
            })
            .FirstOrDefaultAsync(token);
        return shipment ?? throw new InvalidOperationException("Shipment not found");
    }

    public async Task CreateAsync(CreateShipmentViewModel model, CancellationToken token)
    {
        var shipment = new Shipment
        {
            Carrier = model.Carrier,
            TrackingNumber = model.TrackingNumber,
            ShippingDate = model.ShippingDate,
            EstimatedDeliveryDate = model.EstimatedDeliveryDate,
            ShippingCost = model.ShippingCost,
            OrderId = model.OrderId
        };
        await _context.Shipments.AddAsync(shipment, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task Update(UpdateShipmentViewModel model, CancellationToken token)
    {
        var shipment = await _context.Shipments.FindAsync(model.Id, token) ?? throw new InvalidOperationException("Shipment not found");

        shipment.Carrier = model.Carrier;
        shipment.TrackingNumber = model.TrackingNumber;
        shipment.ShippingDate = model.ShippingDate;
        shipment.EstimatedDeliveryDate = model.EstimatedDeliveryDate;
        shipment.ShippingCost = model.ShippingCost;

        await _context.SaveChangesAsync(token);
    }

    public async Task Delete(int shipmentId, CancellationToken token)
    {
        var shipment = await _context.Shipments
            .FindAsync(shipmentId, token) ?? throw new InvalidOperationException("Shipment not found");

        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync(token);
    }
}
