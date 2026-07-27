using System.Diagnostics;

namespace E_CommerceProject.Repositories.Repositories;

public interface IProductRepository
{
    Task<DatatableResult> GetProductsList(DataTableParamsViewModel dataTableParams, CancellationToken? token);
}

public class ProductRepository(AppDbContext context, ICacheService cacheService) : IProductRepository
{

    private readonly AppDbContext _context = context;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<DatatableResult> GetProductsList(DataTableParamsViewModel dataTableParams, CancellationToken? token = default)
    {
        var cols = new Dictionary<string, Expression<Func<Product, object>>>
        {
            { nameof(ProductViewModel.Product.ProductId), model => model.ProductId},
            { nameof(ProductViewModel.Product.Name), model => model.Name },
            { nameof(ProductViewModel.Product.Description), model => model.Description },
            { nameof(ProductViewModel.Product.Price), model => model.Price },
            { nameof(ProductViewModel.Product.QuantityInStock), model => model.QuantityInStock },
            { nameof(ProductViewModel.Product.CreatedAt), model => model.CreatedAt },
            { nameof(ProductViewModel.Product.Category), model => model.Category!.Name },
            { nameof(ProductViewModel.Product.Discount), model => model.Discount!.Percentage }
        };

        var lst = _context.Products
            .Include(a => a.Category)
            .Include(a => a.Discount)
            .AsNoTracking()
            .AsQueryable();

        if (dataTableParams.SortColumn != null)
        {
            var orderField = cols.ElementAt(int.Parse(dataTableParams.SortColumn));
            lst = dataTableParams.SortColumnDirection == "asc" ? lst.OrderBy(orderField.Value) : lst.OrderByDescending(orderField.Value);
        }
        else
        {
            lst = lst.OrderByDescending(a => a.ProductId);
        }

        if (!string.IsNullOrEmpty(dataTableParams.SearchValue))
        {
            lst = lst.Where(m => m.Name.Contains(dataTableParams.SearchValue)
                || m.Description.Contains(dataTableParams.SearchValue)
                || m.Price.ToString().Contains(dataTableParams.SearchValue)
                || m.QuantityInStock.ToString().Contains(dataTableParams.SearchValue)
                || m.Category!.Name.Contains(dataTableParams.SearchValue)
                || m.Discount!.Name.Contains(dataTableParams.SearchValue)
            );
        }

        var count = await lst.CountAsync();
        var stopwatch = Stopwatch.StartNew();
        // Cache the data in memory to avoid multiple database calls
        var data = await _cacheService.GetOrSetAsync(
            "products",
            async (ct) => await lst.Skip(dataTableParams.Skip).Take(dataTableParams.PageSize).ToListAsync(ct),
            TimeSpan.FromMinutes(1),
            token?? CancellationToken.None
            );
        stopwatch.Stop();
        Console.WriteLine($"Time taken to retrieve data from cache: {stopwatch.ElapsedMilliseconds} ms");
        //var data = await lst.Skip(dataTableParams.Skip).Take(dataTableParams.PageSize).ToListAsync();

        var oResult = new DatatableResult
        {
            draw = dataTableParams.Draw
        };
        foreach (var item in data)
        {
            oResult.data.Add(item);
        }
        oResult.recordsTotal = count;
        oResult.recordsFiltered = count;
        return oResult;
    }
}
