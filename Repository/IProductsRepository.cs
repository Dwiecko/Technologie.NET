using System;

namespace DeliverySystem.Repositories
{
    interface IProductsRepository
    {
        void Add(Product member);
        IEnumerable<Product> GetAll();
        void Update(Product member);
        void Delete(Product member);     
    }
}