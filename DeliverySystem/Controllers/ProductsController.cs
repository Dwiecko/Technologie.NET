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
using DeliverySystem.Repositories;

namespace DeliverySystem.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class ProductsController : Controller
    {
        private IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
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
        public ActionResult Create([Bind("ProductID,Name")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productsRepository.Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            Product product = _productsRepository.Get(id);
            if (id != product.ProductID)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ProductID,Name")] Product product)
        {
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productsRepository.Edit(product);
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
        public ActionResult DeleteConfirmed(int id)
        {
            _productsRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
