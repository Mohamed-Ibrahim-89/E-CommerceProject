namespace E_CommerceProject.Repositories.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Gets the list of users based on the provided DataTable parameters.
    /// </summary>
    /// <param name="dataTableParams"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DatatableResult> GetList(DataTableParamsViewModel dataTableParams, CancellationToken token);
    Task<UserDetailsViewModel> GetById(string userId, CancellationToken token);
}

public class UserRepository(UserManager<User> userManager, AppDbContext context) : IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly AppDbContext _context = context;

    public async Task<DatatableResult> GetList(DataTableParamsViewModel dataTableParams, CancellationToken token)
    {
        var usersQuery = _userManager.Users.AsQueryable();

        // Apply search filter if provided
        if (!string.IsNullOrEmpty(dataTableParams.SearchValue))
        {
            var searchValue = dataTableParams.SearchValue.ToLower();
            usersQuery = usersQuery
                .Where(u => u.UserName!.ToLower().Contains(searchValue) 
                    || u.Email!.ToLower().Contains(searchValue));
        }

        // Apply sorting
        if (dataTableParams.SortColumn != null && dataTableParams.SortColumn.Any())
        {
            var orderColumnIndex = dataTableParams.SortColumn.First();
            var orderDirection = dataTableParams.SortColumnDirection;

            usersQuery = orderColumnIndex switch
            {
                // Username column
                '0' => orderDirection == "asc"
                                        ? usersQuery.OrderBy(u => u.UserName)
                                        : usersQuery.OrderByDescending(u => u.UserName),
                // Email column
                '1' => orderDirection == "asc"
                                        ? usersQuery.OrderBy(u => u.Email)
                                        : usersQuery.OrderByDescending(u => u.Email),
                _ => usersQuery.OrderBy(u => u.UserName),// Default sorting
            };
        }

        // Apply pagination
        var totalRecords = await usersQuery.CountAsync(token);
        var pagedUsers = await usersQuery
            .Skip(dataTableParams.Skip)
            .Take(dataTableParams.PageSize)
            .ToListAsync(token);

        // Map to UserViewModel
        var userViewModels = new List<UserViewModel>();
        foreach (var user in pagedUsers)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userViewModels.Add(new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                Roles = roles
            });
        }

        var oResult = new DatatableResult
        {
            draw = dataTableParams.Draw
        };

        foreach (var item in userViewModels)
            oResult.data.Add(item);

        oResult.recordsTotal = totalRecords;
        oResult.recordsFiltered = totalRecords;

        return oResult;


    }

    public async Task<UserDetailsViewModel> GetById(string userId, CancellationToken token)
    {
        var user = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new User
            {
                UserName = u.UserName,
                Email = u.Email,
                FullName = u.FullName
            })
            .FirstOrDefaultAsync(token);

        var customer = await _context.CustomerInfo
            .FirstOrDefaultAsync(c => c.UserId == userId, token);

        return new UserDetailsViewModel
        {
            Username = user.UserName!,
            Email = user.Email!,
            FullName = user.FullName,
            DateOfBirth = customer?.DateOfBirth,
            PhoneNumber = customer?.PhoneNumber!,
            AddressLine1 = customer?.AddressLine1,
            AddressLine2 = customer?.AddressLine2,
            Country = customer?.Country,
            City = customer?.City,
            Landmark = customer?.Landmark,
            ZipCode = customer?.ZipCode,
            State = customer?.State
        };
    }
}
