using System.Collections.Generic;
using System.Linq;
using DeliverySystem.Models;

namespace DeliverySystem.Repository.Doubles
{
    public interface ICategoriesRepository
    {
        void Create(Category category);
        Category Get(int? id);
        IEnumerable<Category> GetAll();
        void Update(Category entity);
        void Delete(int? id);
    }
    public class FakeCategoryRepository : ICategoriesRepository
    {
        private HashSet<Category> _categories = new HashSet<Category>();

        public void Create(Category category)
        {
            _categories.Add(category);
        }

        public void Delete(int? id)
        {
            var category = _categories.SingleOrDefault(m => m.Id == id);
            _categories.Remove(category);
        }

        public Category Get(int? id)
        {
            var category = _categories.FirstOrDefault(del => del.Id == id);
            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.AsEnumerable();
        }

        public void Update(Category entity)
        {
           var categoryToUpdate = _categories.FirstOrDefault(ent => ent.Id == entity.Id);
            if(categoryToUpdate != null)
            {
                categoryToUpdate = entity;
            }
        }
    }
}
