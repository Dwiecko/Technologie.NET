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
            var products = _productsRepository.GetAll();
            return View(products);
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            var product = _productsRepository.Get(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductID,Name")] Product product)
        {
            _productsRepository.Add(product);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductID,Name")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }
            
            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Product product)
        {
            _productsRepository.Delete(product.ProductID);
            return Redirect("Index");
        }
    }
}
