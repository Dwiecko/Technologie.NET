using DeliverySystem.Data;
using DeliverySystem.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverySystem.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(int? id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is empty");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is empty");
            }
            
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var entity = Get(id);
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is empty");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

    }
}
