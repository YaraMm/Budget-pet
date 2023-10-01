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

        private const string _CategoryFilePath = "categories.json";

        public void CategoriesSaveChanges()
        {
            var CategoryJson = JsonConvert.SerializeObject(LCategories);
            File.WriteAllText(_CategoryFilePath, CategoryJson);
        }
        public void CategoriesLoadBuffer() 
        {
            var jsonCategory = File.ReadAllText(_CategoryFilePath);
            LCategories = JsonConvert.DeserializeObject<List<Category>>(jsonCategory);
        }
        
        
    }
}
