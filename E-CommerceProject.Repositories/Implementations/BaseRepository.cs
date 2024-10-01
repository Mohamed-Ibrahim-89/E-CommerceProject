using E_CommerceProject.Repositories.Data;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        // It'll be done by Ahmed Medhat
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        // It'll be done by Ahmed Ibrahim
        public async Task<T> AddItem(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        // It'll be done by Ibrahim Khalil
        public async Task<T> UpdateItem(T item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        // It'll be done by Mostafa Hamed
        public async Task DeleteItem(int id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
