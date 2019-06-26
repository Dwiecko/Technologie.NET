namespace DeliverySystem.Controllers
{
    #region Usings

    using DeliverySystem.Models;
    using DeliverySystem.Repository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    [Authorize(Roles = "Administrator, User")]
    public class ProductsController : Controller
    {
        #region Fields and constructors

        private IRepository<Product> _productsRepository;

        public ProductsController(IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        #endregion

        #region Methods

        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productsRepository.GetAll();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productsRepository.Get(id);
            if (product == null) return NotFound();

            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsRepository.Create(product);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _productsRepository.Get(id);
            if (id != product.Id)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] Product product)
        {
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productsRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var product = await _productsRepository.Get(id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _productsRepository.Get(id);
            if (product == null)
            {
                return BadRequest();
            }
            await _productsRepository.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
