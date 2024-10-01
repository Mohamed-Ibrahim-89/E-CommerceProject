using E_CommerceProject.Repositories.Data;
using E_CommerceProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_CommerceProject.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        // It'll be done by Mohamed Ibrahim
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? criteria = null, string[]? includes = null)
        {
            return await _dbSet.ToListAsync();
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
