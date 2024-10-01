using System.Linq.Expressions;

namespace E_CommerceProject.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? criteria = null, string[]? includes = null);
        Task<T> GetById(Expression<Func<T, bool>> caretiria, string[]? Includes = null);
        public Task<T> AddItem(T item);
        public Task<T> UpdateItem(T item);
        public Task DeleteItem(int id);
    }
}
