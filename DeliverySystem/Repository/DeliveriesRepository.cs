namespace DeliverySystem.Repository
{
    #region Usings

    using DeliverySystem.Data;
    using DeliverySystem.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    public interface IDeliveriesRepository
    {
        Task Create(Delivery delivery);
        Task<Delivery> Get(int? id);
        Task<IEnumerable<Delivery>> GetAll();
        Task Update(Delivery entity);
        Task Delete(int? id);
    }
    public class DeliveriesRepository : IDeliveriesRepository
    {
        #region Fields and Constructors

        private readonly ApplicationDbContext _context;

        public DeliveriesRepository(ApplicationDbContext context) => _context = context;

        #endregion

        #region Methods

        // GET: Deliveries1/Details/5
        public async Task<Delivery> Get(int? id)
        {
            return await _context.Delivery.Include(x => x.Category).Include(y => y.Product).FirstOrDefaultAsync(z => z.Id == id);
        }
        public async Task<IEnumerable<Delivery>> GetAll()
        {
            return await _context.Delivery.Include(x => x.Category).Include(y => y.Product).ToAsyncEnumerable().ToList();
        }

        public async Task Create(Delivery delivery)
        {
            _context.Delivery.Add(delivery);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Delivery delivery)
        {
            _context.Delivery.Update(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var delivery = _context.Delivery
                .Include(d => d.Category)
                .Include(d => d.Product)
                .SingleOrDefault(m => m.Id == id);

            _context.Remove(delivery);

            await _context.SaveChangesAsync();
        }
        #endregion
    }
}