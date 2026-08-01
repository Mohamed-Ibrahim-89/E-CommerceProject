using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_CommerceProject.Repositories.Repositories;

public interface IProductRepository
{
    /// <summary>
    /// Get the list of products with pagination, sorting, and searching capabilities.
    /// </summary>
    /// <param name="dataTableParams"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DatatableResult> GetListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token);

    /// <summary>
    /// Get the list of products with pagination and searching capabilities.
    /// </summary>
    /// <param name="dataTableParams"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DatatableResult> GetListForHomePageAsync(DataTableParamsViewModel dataTableParams, CancellationToken token);

    /// <summary>
    /// Get a product by its ID.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Product> GetEntity(int productId);

    /// <summary>
    /// Get product details by its ID.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ProductDetailsViewModel> GetByIdAsync(int productId, CancellationToken token);

    /// <summary>
    /// Add a new product to the database.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddAsync(CreateProductViewModel model, CancellationToken token);

    /// <summary>
    /// Update a product in the database.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task UpdateAsync(EditProductViewModel model, CancellationToken token);

    /// <summary>
    /// Delete a product from the database.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task DeleteAsync(int productId, CancellationToken token);
}

public class ProductRepository(AppDbContext context
    , IUploadFile uploadFile
    , IHostingEnvironment hostingEnvironment
    ) : IProductRepository
{
    private readonly AppDbContext _context = context;
    private readonly IUploadFile _uploadFile = uploadFile;
    private readonly IHostingEnvironment _hostingEnvironment = hostingEnvironment;

    public async Task<DatatableResult> GetListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token = default)
    {
        var cols = new Dictionary<string, Expression<Func<Product, object>>>
        {
            { nameof(ProductViewModel.ProductId), model => model.Id},
            { nameof(ProductViewModel.Name), model => model.Name },
            { nameof(ProductViewModel.Description), model => model.Description },
            { nameof(ProductViewModel.Price), model => model.Price },
            { nameof(ProductViewModel.QuantityInStock), model => model.QuantityInStock },
            { nameof(ProductViewModel.CreatedAt), model => model.CreatedAt },
            { nameof(ProductViewModel.Category), model => model.Category!.Name },
            { nameof(ProductViewModel.Discount), model => model.Discount!.Percentage }
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
            lst = lst.OrderByDescending(a => a.Id);
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

        var data = await lst
            .Select(a => new ProductViewModel
            {
                ProductId = a.Id,
                Name = a.Name,
                Description = a.Description,
                Price = a.Price,
                QuantityInStock = a.QuantityInStock,
                CreatedAt = a.CreatedAt,
                Category = a.Category!.Name,
                Discount = a.Discount!.Percentage
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

    public async Task<DatatableResult> GetListForHomePageAsync(DataTableParamsViewModel dataTableParams, CancellationToken token)
    {
        var lst = _context.Products
            .Include(a => a.Category)
            .Include(a => a.Discount)
            .AsNoTracking()
            .AsQueryable();

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

        if (int.TryParse(dataTableParams.CategoryId, out int id) && id > 0)
            lst = lst.Where(p => p.CategoryId == id);

        var count = await lst.CountAsync();

        var data = await lst
            .Select(a => new ProductDetailsViewModel
            {
                ProductId = a.Id,
                Name = a.Name,
                Description = a.Description,
                Price = a.Price,
                Cover = a.Cover,
                QuantityInStock = a.QuantityInStock,
                Category = a.Category!.Name,
                Discount = a.Discount!.Percentage
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

    public async Task<ProductDetailsViewModel> GetByIdAsync(int productId, CancellationToken token = default)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Discount)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId, token);

        return product == null ? throw new InvalidOperationException($"Product with ID {productId} not found.") : new ProductDetailsViewModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Cover = product.Cover,
            QuantityInStock = product.QuantityInStock,
            Category = product.Category?.Name ?? string.Empty,
            Discount = product.Discount?.Percentage ?? 0
        };
    }

    public async Task<Product> GetEntity(int productId) =>
        await _context.Products.FindAsync(productId)
        ?? throw new InvalidOperationException("Product not found");

    public async Task AddAsync(CreateProductViewModel model, CancellationToken token = default)
    {
        if (model.File != null)
        {
            string filePath = await _uploadFile.UploadFileAsync("\\Images\\Product\\", model.File);
            model.Cover = filePath;
        }
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Cover = model.Cover,
            QuantityInStock = model.QuantityInStock,
            CategoryId = model.CategoryId,
            DiscountId = model.DiscountId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(EditProductViewModel model, CancellationToken token)
    {
        var product = await GetEntity(model.ProductId) ?? throw new Exception("Product not found");

        if (model.File != null)
        {
            DeleteImage(product.Cover);
            string filePath = await _uploadFile.UploadFileAsync("\\Images\\Product\\", model.File);
            model.Cover = filePath;
        }

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.Cover = model.Cover;
        product.QuantityInStock = model.QuantityInStock;
        product.CategoryId = model.CategoryId;
        product.DiscountId = model.DiscountId;

        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(int productId, CancellationToken token)
    {
        var product = await GetEntity(productId) ?? throw new Exception("Product not found");

        string imagePath = product.Cover;
        DeleteImage(imagePath);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(token);
    }

    private void DeleteImage(string imagePath)
    {
        if (!string.IsNullOrEmpty(imagePath))
        {
            string fullPath = _hostingEnvironment.WebRootPath + imagePath;

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}