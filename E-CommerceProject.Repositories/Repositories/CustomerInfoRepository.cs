namespace E_CommerceProject.Repositories.Repositories;

public interface ICustomerInfoRepository
{
    Task<CustomerInfoViewModel> GetByIdAsync(string userId, CancellationToken token);
    Task AddAsync(CustomerInfoViewModel model, string userId, CancellationToken token);
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

    public async Task AddAsync(CustomerInfoViewModel model, string userId, CancellationToken token)
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
    }
}
