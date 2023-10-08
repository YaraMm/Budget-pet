using MyBudget.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.services
{
    internal class CategoriesService
    {
        public List<Category> LCategories { get; set; }

        private const string CategoryFilePath = "categories.json";

        public void CategoriesSaveChanges()
        {
            var CategoryJson = JsonConvert.SerializeObject(LCategories);
            File.WriteAllText(CategoryFilePath, CategoryJson);
        }
        public void CategoriesLoadBuffer() 
        {
            if (!File.Exists(CategoryFilePath))
            {
                LCategories = new List<Category>();
                CategoriesSaveChanges();
                return;
            }
            var jsonCategory = File.ReadAllText(CategoryFilePath);
            LCategories = JsonConvert.DeserializeObject<List<Category>>(jsonCategory);
        }
        
        
    }
}
