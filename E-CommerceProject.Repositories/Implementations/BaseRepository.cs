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
            _dbSet = context.Set<T>();
        }
        // It'll be done by Mohamed Ibrahim
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? criteria = null, string[]? includes = null)
        {
            throw new NotImplementedException();
        }
        // It'll be done by Ahmed Medhat
        public async Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }
        // It'll be done by Ahmed Ibrahim
        public async Task<T> AddItem(T item)
        {
            throw new NotImplementedException();
        }
        // It'll be done by Ibrahim Khalil
        public async Task<T> UpdateItem(T item)
        {
            throw new NotImplementedException();
        }
        // It'll be done by Mostafa Hamed
        public async Task DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

    }
}
