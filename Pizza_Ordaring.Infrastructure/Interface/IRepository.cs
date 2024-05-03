using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordaring.Infrastructure.Interface
{
     public interface IRepository<T> where T : class
    {
        T Add(T entity);
        IEnumerable<T> FindAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> Remove(int id);
        void SaveChanges();
        Task<T> Update(T entity);
    }
}
