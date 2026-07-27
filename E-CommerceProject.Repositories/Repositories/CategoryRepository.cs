namespace E_CommerceProject.Repositories.Repositories;

public interface ICategoryRepository
{
    Task<DatatableResult> GetCategoriesList(DataTableParamsViewModel dataTableParams);
}

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<DatatableResult> GetCategoriesList(DataTableParamsViewModel dataTableParams)
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
                            .ToListAsync();

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
