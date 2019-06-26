namespace DeliverySystem.Repositories
{
    #region Usings

    using DeliverySystem.Data;
    using DeliverySystem.Repository;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        #region Fields and constructors

        private readonly ApplicationDbContext context;
        private DbSet<T> entities;

        #endregion

        #region Methods

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToAsyncEnumerable().ToList();
        }

        public async Task<T> Get(int? id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is empty");
            }
            entities.Add(entity);

            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is empty");
            }

            entities.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is empty");
            }
            entities.Remove(entity);

            await context.SaveChangesAsync();
        }
        #endregion
    }
}