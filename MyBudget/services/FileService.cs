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
    internal class FileService
    {
        public List<Note>  Notes { get; set; }

        private const string _filePath = "data.json";

        public void SaveChanges() 
        {
            var ourJsonFile = JsonConvert.SerializeObject(Notes);
            File.WriteAllText(_filePath, ourJsonFile);
        }

        public void LoadBuffer()//Берет БД на буфер
        {
            if (!File.Exists(_filePath))
            {
                Notes = new List<Note>();
                SaveChanges();
                return;
            }

            var jsonData = File.ReadAllText(_filePath);
            Notes = JsonConvert.DeserializeObject<List<Note>>(jsonData);
        }
    }
}
