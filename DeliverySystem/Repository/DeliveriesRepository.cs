using DeliverySystem.Data;
using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DeliverySystem.Repository
{
    public interface IDeliveriesRepository
    {
        void Create(Delivery delivery);
        Delivery Get(int? id);
        IEnumerable<Delivery> GetAll();
        void Update(Delivery entity);
        void Delete(int? id);
    }
    public class DeliveriesRepository: IDeliveriesRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deliveries1/Details/5
        public Delivery Get(int? id)
        {
           return _context.Delivery.Include(x => x.Category).Include(y => y.Product).FirstOrDefault(z => z.Id == id);
        }
        public IEnumerable<Delivery> GetAll()
        {
            return _context.Delivery.Include(x => x.Category).Include(y => y.Product).AsEnumerable();
        }

        public void Create(Delivery delivery)
        {
            _context.Delivery.Add(delivery);
            _context.SaveChanges();
        }

     
        public void Update(Delivery delivery)
        {
            _context.Delivery.Update(delivery);
            _context.SaveChanges();
        }


        public void Delete(int? id)
        {
            var delivery = _context.Delivery
                .Include(d => d.Category)
                .Include(d => d.Product)
                .SingleOrDefault(m => m.Id == id);
            _context.Remove(delivery);
            _context.SaveChanges();
        }
    }
}
