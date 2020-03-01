using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Domain.Services
{
    public interface IAsyncRepository<T>
    {
        Task<T[]> GetAsync();

        Task<T> GetAsync(object id);

        Task<T> InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
