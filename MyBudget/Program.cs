using MyBudget.models;
using MyBudget.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MyBudget
{
    class Program
    {
        static RepositoryOfCategories repositoryOfCategories;
        static RepositoryOfNotes repositoryOfNotes;
        static void Main(string[] args)
        {
            repositoryOfCategories = new RepositoryOfCategories();
            repositoryOfNotes = new RepositoryOfNotes();    

            Console.WriteLine("Выберите действие:\n1 - Новая заметка\n2 - Вывести статистику\n");
            //RepCat.SaveCategory(CreateACategory("dadad"));
            //Console.WriteLine("Введите exit для выхода");
                //var action = new FileService();

                string choise = Console.ReadLine();
                if (choise == "1")
                {
                    CreateANewNote();
                }
                else if (choise == "2")
                {
                    GetStatistic();
                }
            return;
        }

        public static void GetStatistic()
        {
            var service = new FileService();
            service.LoadBuffer();
            foreach (var note in service.Notes)
            {
                var nameCat = repositoryOfCategories.GetByID(note.Category);
                Console.WriteLine($"{nameCat.Name} {note.Amount} рублей {note.Date}\n");
            }
        }
        public static void CreateANewNote()
        {
            Note note = CreateNote();

            Console.WriteLine("Выберите катеогорию:");
            Console.WriteLine("n - Создать категорию\n");
            ShowCatefories();

            string choice = Console.ReadLine();
            if (choice == "n")
            {
               CreateACategory(note); 
            }
            else
            {
                var choiceNum = Int32.Parse(choice);
                note.Category = ChooseACategory(choiceNum);
            }
            
            Console.WriteLine("Выведите сумму:\n");
            note.Amount = decimal.Parse(Console.ReadLine());
            
            repositoryOfNotes.Save(note);
        }
        public static void ShowCatefories()
        {
            var listOfCategories = repositoryOfCategories.GetAll();
            for (int i = 0; i < listOfCategories.Count; i++)
            {
                Console.WriteLine(i + 1 + "  " + listOfCategories[i].Name.ToString());
            }
        }
        public static Guid ChooseACategory(int choice)
        {
                var listOfCategories = repositoryOfCategories.GetAll();
                //choice = Int32.Parse(Console.ReadLine());
                var res = listOfCategories[choice - 1].Id;
                return res;
        }

        public static string CategoriesListToString(List<Category> list)
        {
            string res = "";
            foreach (var x in list)    res += x.Name.ToString() + "\n";
            return res;
        }

        public static void CreateACategory (Note note)
        {
            Console.WriteLine("Введите имя категории");
            var name = Console.ReadLine();
            var cat = new Category() { Name = name };
            var idCategory = repositoryOfCategories.SaveCategory(cat);
            note.Category = idCategory;
        }
        public static Note CreateNote()
        {
            return new Note() { Date = DateTime.Now};
        }
    }
}
