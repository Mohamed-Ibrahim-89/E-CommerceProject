using E_CommerceProject.Repositories.Data;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_CommerceProject.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? criteria = null, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (criteria != null) 
                return await query.Where(criteria).ToListAsync();

            if (includes != null) 
            { 
                foreach (var include in includes)
                {
                    query = query.Include(include).AsSplitQuery();
                }
                return await query.ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> AddItem(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateItem(T item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

    }
}
