using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Authorization;
using DeliverySystem.Repository.Doubles;

namespace DeliverySystem.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class CategoriesController : Controller
    {
        private ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // GET: Categorys
        [HttpGet]
        public IActionResult Index()
            
        {
            throw new System.Exception();
           // return View(_categoriesRepository.GetAll().AsEnumerable());
        }

        // GET: Categorys/Details/5
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var category = _categoriesRepository.Get(id);
            if (category == null) return NotFound();

            return View(category);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.Create(category);
                //Add(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            Category category = _categoriesRepository.Get(id);
            if (id != category.Id)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categorys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name")] Category category)
        {
            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _categoriesRepository.Update(category);
                //Edit(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: Categorys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category category = _categoriesRepository.Get(id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category category = _categoriesRepository.Get(id);
            if (category == null)
            {
                return BadRequest();
            }
            _categoriesRepository.Delete(id);
            //Delete(id);
            return RedirectToAction("Index");
        }
    }
}
