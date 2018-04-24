using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Authorization;
using DeliverySystem.Models.DeliveryViewModels;
using DeliverySystem.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeliverySystem.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class DeliveriesController : Controller
    {
        private IDeliveriesRepository _deliveriesRepository;
        private IRepository<Category> _categoriesRepo;
        private IRepository<Product> _productsRepo;

        public DeliveriesController(IDeliveriesRepository deliveriesRepository,
                                    IRepository<Category> categoriesRepo,
                                    IRepository<Product> productsRepo)
        {
            _deliveriesRepository = deliveriesRepository;
            _categoriesRepo = categoriesRepo;
            _productsRepo = productsRepo;
    }

        // GET: Deliveries
        public IActionResult Index()
        {
            ViewBag.Test = CountAverageForAmounts();
            return View(_deliveriesRepository.GetAll());
        }

        // GET: Deliveries/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var delivery = _deliveriesRepository.Get(id);
            return View(delivery);
        }

        // GET: Deliveries/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_categoriesRepo.GetAll(), "Id", "Name");
            ViewData["ProductID"] = new SelectList(_productsRepo.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _deliveriesRepository.Create(delivery);
                return View(delivery);
            }
            ViewData["CategoryID"] = new SelectList(_categoriesRepo.GetAll(), "Id", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(_productsRepo.GetAll(), "Id", "Name", delivery.ProductID);
            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = _deliveriesRepository.Get(id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_categoriesRepo.GetAll(), "Id", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(_productsRepo.GetAll(), "Id", "Name", delivery.ProductID);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id, [Bind("Id,ProductID,CategoryID,DeliveryStart,DeliveryEnd,PhoneNumber,City,StreetName,EstimatedWeight,Amount")] Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_categoriesRepo.GetAll(), "Id", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(_productsRepo.GetAll(), "Id", "Name", delivery.ProductID);
            return View(delivery);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Deliveries/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = _deliveriesRepository.Get(id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var delivery = _deliveriesRepository.Get(id);
            _deliveriesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _deliveriesRepository.GetAll().Any(e => e.Id == id);
        }


        public ActionResult CountAverageForAmounts()
        {
            var result = _deliveriesRepository.GetAll().Select(x => x.Amount).ToList();

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
