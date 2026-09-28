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
}

public class  OrderRepository(AppDbContext context) : IOrderRepository
{
    private readonly AppDbContext _context = context;
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
                PhoneNumber = a.User!.PhoneNumber!
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
}
