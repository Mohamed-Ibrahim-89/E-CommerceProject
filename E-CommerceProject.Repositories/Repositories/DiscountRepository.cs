namespace E_CommerceProject.Repositories.Repositories;

public interface IDiscountRepository
{
    Task<IEnumerable<IdNameViewModel>> GetDiscountsAsDropDown();
}

public class DiscountRepository(AppDbContext context) : IDiscountRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<IdNameViewModel>> GetDiscountsAsDropDown()
    {
        return await _context.Discounts
            .OrderBy(d => d.Percentage)
            .Select(d => new IdNameViewModel
            {
                Id = d.DiscountId,
                Name = $"{d.Percentage}%"
            })
            .ToListAsync();
    }
}
