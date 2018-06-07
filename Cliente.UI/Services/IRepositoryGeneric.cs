using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.UI.Services
{
    public interface IRepositoryGeneric<T> where T : class
    {

        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(object id);
        Task<bool> InsertAsync(T insert);
        Task<bool> UpdateAsync(object id, T updated);
        Task<bool> RemoveAsync(object id);

        List<T> GetAll();
        bool Insert(T insert);
        T Get(object id);
        bool Update(object id, T updated);
        bool Remove(object id);
    }
}
