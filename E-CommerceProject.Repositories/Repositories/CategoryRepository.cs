using E_CommerceProject.Entities.ViewModels.Categories;

namespace E_CommerceProject.Repositories.Repositories;

public interface ICategoryRepository
{
    /// <summary>
    /// Get Categories List
    /// </summary>
    /// <param name="dataTableParams"></param>
    /// <returns></returns>
    Task<DatatableResult> GetListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token);

    /// <summary>
    /// Get Category by Id
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<CategoryViewModel> GetByIdAsync(int categoryId, CancellationToken token);

    /// <summary>
    /// Add a new category to the database.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddAsync(CreateCategoryViewModel model, CancellationToken token);

    /// <summary>
    /// Update a category in the database.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task UpdateAsync(EditCategoryViewModel model, CancellationToken token);

    /// <summary>
    /// Delete a category from the database.
    /// </summary>
    /// <param name="categorytId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task DeleteAsync(int categoryId, CancellationToken token);

    /// <summary>
    /// Get Categories As DropDown
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<IdNameViewModel>> GetCategoryAsDropDown();
}

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<DatatableResult> GetListAsync(DataTableParamsViewModel dataTableParams, CancellationToken token = default)
    {
        var cols = new Dictionary<string, Expression<Func<Category, object>>>
        {
            { nameof(CategoryViewModel.CategoryId), model => model.CategoryId},
            { nameof(CategoryViewModel.Name), model => model.Name },
            { nameof(CategoryViewModel.CreatedAt), model => model.CreatedAt }
        };

        var lst = _context.Categories
                          .AsNoTracking()
                          .AsQueryable();
        if (dataTableParams.SortColumn != null)
        {
            var orderField = cols.ElementAt(int.Parse(dataTableParams.SortColumn));
            lst = dataTableParams.SortColumnDirection == "asc" ? lst.OrderBy(orderField.Value) : lst.OrderByDescending(orderField.Value);
        }
        else
        {
            lst = lst.OrderByDescending(a => a.CategoryId);
        }

        if (!string.IsNullOrEmpty(dataTableParams.SearchValue))
        {
            lst = lst.Where(m => m.Name.Contains(dataTableParams.SearchValue));
        }

        var count = await lst.CountAsync();
        var data = await lst.Skip(dataTableParams.Skip)
                            .Take(dataTableParams.PageSize)
                            .Select(a => new CategoryViewModel
                            {
                                CategoryId = a.CategoryId,
                                Name = a.Name,
                                CreatedAt = a.CreatedAt
                            })
                            .ToListAsync(token);

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

    public async Task<CategoryViewModel> GetByIdAsync(int categoryId, CancellationToken token)
    {
        var category =  await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.CategoryId == categoryId, token);

        return category == null ? throw new InvalidOperationException($"Category with ID {categoryId} not found.") : new CategoryViewModel
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            CreatedAt = category.CreatedAt
        };
    }

    public async Task AddAsync(CreateCategoryViewModel model, CancellationToken token)
    {
        if(await _context.Categories.AnyAsync(c => c.Name == model.Name))
            throw new InvalidOperationException($"Category with name '{model.Name}' already exists.");

        var category = new Category
        {
            Name = model.Name,
            CreatedAt = DateTime.Now
        };

        await _context.Categories.AddAsync(category, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(EditCategoryViewModel model, CancellationToken token)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId  == model.CategoryId) ?? throw new InvalidOperationException("Category does not exsit!");

        if (await _context.Categories.AnyAsync(c => c.Name == model.Name))
            throw new InvalidOperationException($"Category with name {model.Name} already exists.");

        category.Name = model.Name;
        category.CreatedAt = DateTime.Now;
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(int categoryId, CancellationToken token)
    {
        var viewModel = await GetByIdAsync(categoryId, token) ?? throw new InvalidOperationException("Category does not exsit!");

        var category = new Category()
        {
            CategoryId = viewModel.CategoryId,
            Name = viewModel.Name,
            CreatedAt = viewModel.CreatedAt
        };

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<IdNameViewModel>> GetCategoryAsDropDown()
    {
        var categories = await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new IdNameViewModel
            {
                Id = c.CategoryId,
                Name = c.Name
            })
        .ToListAsync();

        return categories;
    }
}
