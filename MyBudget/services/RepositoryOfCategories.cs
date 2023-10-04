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
        private readonly CategoriesService _CategoryService;

        public RepositoryOfCategories()
        {
            _CategoryService = new CategoriesService();
            _CategoryService.CategoriesLoadBuffer();
        }
        
        public List<Category> GetAll()
        { 
            return _CategoryService.LCategories;
        }

        public Category GetByID(Guid id)
        {
            foreach (var x in _CategoryService.LCategories)
            {
                if (x.Id == id) return x;
            }
            return null;
        }
        public Guid SaveCategory(Category Category)
        {
            _CategoryService.CategoriesLoadBuffer();
            Category.Id = Guid.NewGuid();
            _CategoryService.LCategories.Add(Category);
            _CategoryService.CategoriesSaveChanges();
            return Category.Id;
        }
    }
}
