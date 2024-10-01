using E_CommerceProject.Entities.Models;
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
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (criteria != null || includes != null)
            {
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include).AsSplitQuery();
                    }
                }

                if (criteria != null)
                {
                    return await query.Where(criteria).ToListAsync();
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> caretiria, string[]? Includes = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include).AsSplitQuery();
                }
            }

            if (caretiria != null)
            {
                return await query.Where(caretiria).FirstOrDefaultAsync();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> AddItem(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateItem(T item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException("Item not found");

            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

    }
}
