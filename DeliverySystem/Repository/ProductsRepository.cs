using DeliverySystem.Data;
using DeliverySystem.Models;
using DeliverySystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DeliverySystem.Repository
{
    class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Product member)
        {
            _context.Product.Add(member);
            _context.SaveChanges();
        }

        public Product Get(int? id)
        {
            var product = _context.Product
                            .SingleOrDefault(m => m.ProductID == id);

            return product;
        }

        public IEnumerable<Product> GetAll() => _context.Product.AsEnumerable();

        public void Edit(Product member)
        {
            _context.Product.Attach(member);
            _context.Entry(member).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Product productEntity = _context.Product.Find(id);
            _context.Product.Remove(productEntity);
            _context.SaveChanges();
        }
    }
}