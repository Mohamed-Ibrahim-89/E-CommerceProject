namespace E_CommerceProject.Repositories.Repositories;

public interface ICustomerInfoRepository
{
    Task<CustomerInfoViewModel> GetByIdAsync(string userId, CancellationToken token);
    Task<int> AddAsync(CustomerInfoViewModel model, string userId, CancellationToken token);
    Task<int> UpdateAsync(CustomerInfoViewModel model, string userId, CancellationToken token);
    Task<bool> ExistsAsync(string userId, CancellationToken token);
}

public class CustomerInfoRepository(AppDbContext context) : ICustomerInfoRepository
{
    private readonly AppDbContext _context = context;

    public async Task<CustomerInfoViewModel> GetByIdAsync(string userId, CancellationToken token)
    {
        var customerInfo = await _context.CustomerInfo
            .Where(ci => ci.UserId == userId)
            .Select(ci => new CustomerInfoViewModel
            {
                Id = ci.Id,
                FirstName = ci.FirstName,
                LastName = ci.LastName,
                DateOfBirth = ci.DateOfBirth,
                PhoneNumber = ci.PhoneNumber,
                AddressLine1 = ci.AddressLine1,
                AddressLine2 = ci.AddressLine2,
                Country = ci.Country,
                City = ci.City,
                Landmark = ci.Landmark,
                ZipCode = ci.ZipCode,
                State = ci.State
            })
            .FirstOrDefaultAsync(token);
        return customerInfo;
    }

    public async Task<int> AddAsync(CustomerInfoViewModel model, string userId, CancellationToken token)
    {
        var customerInfo = new CustomerInfo
        {
            UserId = userId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            PhoneNumber = model.PhoneNumber,
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            Country = model.Country,
            City = model.City,
            Landmark = model.Landmark,
            ZipCode = model.ZipCode,
            State = model.State
        };

        await _context.CustomerInfo.AddAsync(customerInfo, token);
        await _context.SaveChangesAsync(token);

        return customerInfo.Id;
    }

    public async Task<int> UpdateAsync(CustomerInfoViewModel model, string userId, CancellationToken token)
    {
        var customerInfo = await _context.CustomerInfo
            .FirstOrDefaultAsync(ci => ci.UserId == userId, token);
        if (customerInfo == null)
            throw new InvalidOperationException("Customer info not found");

        customerInfo.FirstName = model.FirstName;
        customerInfo.LastName = model.LastName;
        customerInfo.DateOfBirth = model.DateOfBirth;
        customerInfo.PhoneNumber = model.PhoneNumber;
        customerInfo.AddressLine1 = model.AddressLine1;
        customerInfo.AddressLine2 = model.AddressLine2;
        customerInfo.Country = model.Country;
        customerInfo.City = model.City;
        customerInfo.Landmark = model.Landmark;
        customerInfo.ZipCode = model.ZipCode;
        customerInfo.State = model.State;

        _context.CustomerInfo.Update(customerInfo);
        await _context.SaveChangesAsync(token);

        return customerInfo.Id;
    }

    public async Task<bool> ExistsAsync(string userId, CancellationToken token)
    {
        return await _context.CustomerInfo
            .AsNoTracking()
            .AnyAsync(ci => ci.UserId == userId, token);
    }
}
