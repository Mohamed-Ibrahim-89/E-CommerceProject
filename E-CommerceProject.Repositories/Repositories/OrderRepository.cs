namespace E_CommerceProject.Repositories.Repositories;

public interface IOrderRepository
{
    /// <summary>
    /// Get Order List
    /// </summary>
    /// <param name="dataTableParams"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DatatableResult> GetOrderListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token);
    Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId, CancellationToken token);
    Task<int> CreateOrderAsync(CreateOrderViewModel model, string userId, CancellationToken token);
    Task DeleteAsync(int orderId, CancellationToken token);
}

public class  OrderRepository(
    AppDbContext context,
    ICustomerInfoRepository customerInfoRepo
    ) : IOrderRepository
{
    private readonly AppDbContext _context = context;
    private readonly ICustomerInfoRepository _customerInfoRepo = customerInfoRepo;

    public async Task<DatatableResult> GetOrderListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token)
    {
        var cols = new Dictionary<string, Expression<Func<Order, object>>>
        {
            { nameof(OrderViewModel.Status), model => model.Status },
            { nameof(OrderViewModel.OrderDate), model => model.OrderDate },
            { nameof(OrderViewModel.TotalPrice), model => model.TotalPrice },
            { nameof(OrderViewModel.FirstName), model => model.User!.FirstName },
            { nameof(OrderViewModel.LastName), model => model.User!.LastName },
            { nameof(OrderViewModel.PhoneNumber), model => model.User!.PhoneNumber! },
        };

        var lst = _context.Orders
            .Include(a => a.User)
            .AsNoTracking()
            .AsQueryable();

        if(dataTableParams.SortColumn != null)
        {
            var orderField = cols.ElementAt(int.Parse(dataTableParams.SortColumn));
            lst = dataTableParams.SortColumnDirection == "asc" ? lst.OrderBy(orderField.Value) : lst.OrderByDescending(orderField.Value);
        }
        else
            lst = lst.OrderByDescending(a => a.Id);

        if (!string.IsNullOrEmpty(dataTableParams.SearchValue))
        {
            lst = lst.Where(o => o.Status.ToString().Contains(dataTableParams.SearchValue)
                || o.OrderDate.ToString().Contains(dataTableParams.SearchValue)
                || o.User!.FirstName.Contains(dataTableParams.SearchValue)
                || o.User!.LastName.Contains(dataTableParams.SearchValue)
                || o.User!.PhoneNumber!.Contains(dataTableParams.SearchValue));
        }

        var count = await lst.CountAsync();

        var data = await lst.Select(a => new OrderViewModel
            {
                Id = a.Id,
                Status = a.Status.ToString(),
                OrderDate = a.OrderDate,
                TotalPrice = a.TotalPrice,
                FirstName = a.User!.FirstName,
                LastName = a.User!.LastName,
                PhoneNumber = a.CustomerInfo!.PhoneNumber!
            })
            .Take(dataTableParams.PageSize)
            .Skip(dataTableParams.Skip)
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

    public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId, CancellationToken token)
    {
        var order = await _context.Orders
            .Include(c => c.CustomerInfo)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId, token);

        return order == null
            ? throw new InvalidOperationException("Order not found")
            : new OrderDetailsViewModel
            {
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,

                Name = $"{order.CustomerInfo!.FirstName} {order.CustomerInfo!.LastName}",
                PhoneNumber = order.CustomerInfo!.PhoneNumber!,
                AddressLine1 = order.CustomerInfo!.AddressLine1!,
                AddressLine2 = order.CustomerInfo!.AddressLine2!,

                OrderDetails = [.. order.OrderDetails!.Select(o => new OrderDetailViewModel
                {
                    ProductName = o.Product!.Name,
                    Quantity = o.Quantity,
                    Price = o.Price
                })]
            };
    }

    public async Task<int> CreateOrderAsync(CreateOrderViewModel model, string userId, CancellationToken token)
    {
        var cartItems = await _context.Carts
            .AsNoTracking()
            .Include(p => p.Product)
            .ThenInclude(d => d!.Discount)
            .Where(c => c.UserId == userId)
            .ToListAsync(token);

        if(cartItems.Count == 0)
            throw new InvalidOperationException("Your cart is empty, add some items first.");

        var customerInfoId = 0;
        if ( await _customerInfoRepo.ExistsAsync(userId, token))
        {
            customerInfoId =  await _customerInfoRepo.UpdateAsync(model, userId, token);
        }
        else
        {
            customerInfoId = await _customerInfoRepo.AddAsync(model, userId, token);
        }

        var order = new Order
        {
            Status = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow,
            UserId = userId,
            CustomerInfoId = customerInfoId,
            OrderDetails = []
        };

        foreach (var item in cartItems)
        {
            order.OrderDetails.Add(new OrderDetail
            {
                ProductId = item.ProductId,
                Quantity = item.Amount,
                Price = item.Product!.Discount!.Percentage > 0 
                    ? item.Product!.Price - (item.Product!.Price * (item.Product.Discount.Percentage / 100))
                    : item.Product!.Price
            });
        }
        order.TotalPrice = order.OrderDetails.Sum(od => od.Price * od.Quantity);

        await _context.Orders.AddAsync(order, token);
        await _context.SaveChangesAsync(token);
        return order.Id;
    }

    public async Task DeleteAsync(int orderId, CancellationToken token)
    {
        var order = await _context.Orders.FindAsync(orderId, token) ?? throw new InvalidOperationException("Order not found");
        var orderDetails = await _context.OrderIDetails.Where(od => od.OrderId == orderId).ToListAsync(token);

        _context.Orders.Remove(order);
        _context.OrderIDetails.RemoveRange(orderDetails);
        await _context.SaveChangesAsync(token);
    }
}
