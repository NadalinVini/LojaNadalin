using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cliente.UI.Services
{
    public interface IRepositoryGeneric<T> where T : class
    {

        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(object id);
        Task<T> GetByAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        Task<bool> InsertAsync(T insert);
        Task<bool> UpdateAsync(object id, T updated);
        Task<bool> RemoveAsync(object id);

        List<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        bool Insert(T insert);
        T Get(object id);
        bool Update(object id, T updated);
        bool Remove(object id);

        List<T> Filter(Func<T, bool> where);
    }
}
