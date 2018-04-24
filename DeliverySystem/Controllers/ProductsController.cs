using DeliverySystem.Models;
using DeliverySystem.Repositories;
using DeliverySystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DeliverySystem.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class ProductsController : Controller
    {
        private IRepository<Product> _productsRepository;

        public ProductsController(IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: Products
        [HttpGet]
        public IActionResult Index()
        {
            return View(_productsRepository.GetAll().AsEnumerable());
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _productsRepository.Get(id);
            if (product == null) return NotFound();

            return View(product);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productsRepository.Create(product);
                //Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            Product product = _productsRepository.Get(id);
            if (id != product.Id)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name")] Product product)
        {
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productsRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = _productsRepository.Get(id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = _productsRepository.Get(id);
            if (product == null)
            {
                return BadRequest();
            }
            _productsRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
