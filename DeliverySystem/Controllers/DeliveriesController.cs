using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliverySystem.Data;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Authorization;
using DeliverySystem.Models.DeliveryViewModels;

namespace DeliverySystem.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class DeliveriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deliveries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Delivery.Include(d => d.Category).Include(d => d.Product);
            ViewBag.Test = CountAverageForAmounts();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Deliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery
                .Include(d => d.Category)
                .Include(d => d.Product)
                .SingleOrDefaultAsync(m => m.DeliveryID == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Deliveries/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "Name");
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name");
            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryID,ProductID,CategoryID,DeliveryStart,DeliveryEnd,PhoneNumber,City,StreetName,EstimatedWeight,Amount")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name", delivery.ProductID);
            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery.SingleOrDefaultAsync(m => m.DeliveryID == id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name", delivery.ProductID);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryID,ProductID,CategoryID,DeliveryStart,DeliveryEnd,PhoneNumber,City,StreetName,EstimatedWeight,Amount")] Delivery delivery)
        {
            if (id != delivery.DeliveryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.DeliveryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "Name", delivery.ProductID);
            return View(delivery);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Deliveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Delivery
                .Include(d => d.Category)
                .Include(d => d.Product)
                .SingleOrDefaultAsync(m => m.DeliveryID == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Delivery.SingleOrDefaultAsync(m => m.DeliveryID == id);
            _context.Delivery.Remove(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Delivery.Any(e => e.DeliveryID == id);
        }


        public ActionResult CountAverageForAmounts()
        {
            var result = _context.Delivery.Select(x => x.Amount).ToList();

            var averageOccurences = from row in result
                            group row by row into rowsByValue
                            select new CountAverageForWord
                            {
                                Word = rowsByValue.Key,
                                Count = rowsByValue.Count()
                            };

            ViewBag.AverageForAmount = averageOccurences;

            return View();
        }

    }
}
