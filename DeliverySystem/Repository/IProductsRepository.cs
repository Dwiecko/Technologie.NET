using DeliverySystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Repositories
{
    public interface IProductsRepository
    {
        void Add(Product member);
        Product Get(int? id);
        IEnumerable<Product> GetAll();
        void Edit(Product member);
        void Delete(int? id);
    }
}