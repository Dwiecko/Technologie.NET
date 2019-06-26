namespace DeliverySystem.Controllers
{
    #region Usings

    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using DeliverySystem.Models;
    using Microsoft.AspNetCore.Authorization;
    using DeliverySystem.Models.DeliveryViewModels;
    using DeliverySystem.Repository;
    using Microsoft.AspNetCore.Mvc.Rendering;

    #endregion

    [Authorize(Roles = "Administrator, User")]
    public class DeliveriesController : Controller
    {
        #region Fields and Constructors

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

        #endregion

        #region Methods

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
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryID"] = new SelectList(await _categoriesRepo.GetAll(), "Id", "Name");
            ViewData["ProductID"] = new SelectList(await _productsRepo.GetAll(), "Id", "Name");

            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                await _deliveriesRepository.Create(delivery);
                return View(delivery);
            }
            ViewData["CategoryID"] = new SelectList(await _categoriesRepo.GetAll(), "Id", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(await _productsRepo.GetAll(), "Id", "Name", delivery.ProductID);

            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _deliveriesRepository.Get(id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(await _categoriesRepo.GetAll(), "Id", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(await _productsRepo.GetAll(), "Id", "Name", delivery.ProductID);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductID,CategoryID,DeliveryStart,DeliveryEnd,PhoneNumber,City,StreetName,EstimatedWeight,Amount")] Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(await _categoriesRepo.GetAll(), "Id", "Name", delivery.CategoryID);
            ViewData["ProductID"] = new SelectList(await _productsRepo.GetAll(), "Id", "Name", delivery.ProductID);
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

            var delivery = await _deliveriesRepository.Get(id);
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
            await _deliveriesRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DeliveryExists(int id)
        {
            var delivery = await _deliveriesRepository.Get(id);

            return delivery != null;
        }

        public async Task<IActionResult> CountAverageForAmounts()
        {
            var deliveries = await _deliveriesRepository.GetAll();

            var averageOccurences = from row in deliveries.Select(x => x.Amount)
                                    group row by row into rowsByValue
                                    select new CountAverageForWord
                                    {
                                        Word = rowsByValue.Key,
                                        Count = rowsByValue.Count()
                                    };

            ViewBag.AverageForAmount = averageOccurences;

            return View();
        }

        #endregion
    }
}