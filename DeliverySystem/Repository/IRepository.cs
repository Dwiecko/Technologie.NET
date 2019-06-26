using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Repository
{
    public interface IRepository<T>
    {
        Task Create(T entity);
        Task<T> Get(int? id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T entity);
        Task Delete(int? id);
    }
}