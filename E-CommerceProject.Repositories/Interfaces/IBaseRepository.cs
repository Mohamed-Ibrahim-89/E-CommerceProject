using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? criteria = null, string[]? includes = null);
        public Task<T> GetById(int id);
        public Task<T> AddItem(T item);
        public Task<T> UpdateItem(T item);
        public Task DeleteItem(int id);
    }
}
