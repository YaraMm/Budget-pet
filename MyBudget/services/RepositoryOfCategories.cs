using MyBudget.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.services
{
    internal class RepositoryOfCategories
    {
        private readonly CategoriesService _categoryService;

        public RepositoryOfCategories()
        {
            _categoryService = new CategoriesService();
            _categoryService.CategoriesLoadBuffer();
        }
        
        public List<Category> GetAll()
        { 
            return _categoryService.LCategories;
        }

        public Category GetByID(Guid id)
        {
            foreach (var x in _categoryService.LCategories)
            {
                if (x.Id == id) return x;
            }
            return null;
        }
        public Guid SaveCategory(Category category)
        {
            _categoryService.CategoriesLoadBuffer();
            category.Id = Guid.NewGuid();
            _categoryService.LCategories.Add(category);
            _categoryService.CategoriesSaveChanges();
            return category.Id;
        }
        public void RemoveCategory(Guid id)
        {
            _categoryService.CategoriesLoadBuffer();

            var removedCategory = _categoryService.LCategories.FirstOrDefault(x => x.Id == id);

            if (removedCategory == null)
            {
                throw new Exception($"Id {id} не найден");
            }
            _categoryService.LCategories.Remove(removedCategory);
            _categoryService.CategoriesSaveChanges();
        }
    }
}
