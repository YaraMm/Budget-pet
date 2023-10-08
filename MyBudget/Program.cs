using MyBudget.models;
using MyBudget.services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;

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

            while (true)
            {
                Console.WriteLine("Выберите действие:\n" +
                    "1 - Новая заметка\n" +
                    "2 - Вывести статистику\n" +
                    "3 - Удалить запись \n" +
                    "4 - Изменить запись \n" +
                    "5 - Показать категории \n" +
                    "6 - Удалить категорию");

                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        CreateANewNote();
                        break;
                    case "2":
                        GetStatistics();
                        break;
                    case "3":
                        RemoveNote();
                        break;
                    case "4":
                        EditNote();
                        break;
                    case "5":
                        ShowCategories();
                        break;
                    case "6":
                        RemoveCategory();
                        break;
                    default:
                        Console.WriteLine("Введите команду из списка");
                        break;
                }
            }
        }

        public static void EditNote()
        {
            Console.WriteLine("Выберите изменяемую запись");
            GetStatistics();
            var choice = int.Parse(Console.ReadLine()) - 1;
            var listOfNotes = repositoryOfNotes.GetNotes();
            var note = listOfNotes[choice];
            Console.WriteLine("Выберите изменяемый параметр:\n" +
                    "1 - Сумма\n" +
                    "2 - Дата\n" +
                    "3 - Категория");
            string chosenParametre = Console.ReadLine();
            switch (chosenParametre)
            {
                case "1":
                    EditAmount(note);
                    break;
                case "2":
                    EditDate(note);
                    break;
                case "3":
                    EditCategory(note);
                    break;
            }

            repositoryOfNotes.UpdateNote(note);  
        }

        private static void EditAmount(Note note)
        {
            Console.WriteLine("Введите новую сумму:");
            decimal newAmount = decimal.Parse(Console.ReadLine(), new NumberFormatInfo() { NumberDecimalSeparator = "." });
            note.Amount = newAmount; 
        }

        private static void EditDate(Note note)
        {
            Console.WriteLine("Введите новую дату:");
            DateTime newDate = DateTime.Parse(Console.ReadLine());
            note.Date = newDate;
        }

        private static void EditCategory(Note note)
        {
            SetCategory(note);
        }

        private static void RemoveNote()
        {
            Console.WriteLine("Выберите удаляемый элемент");
            var choice = int.Parse(Console.ReadLine()) - 1;
            var listOfNotes = repositoryOfNotes.GetNotes();
            var removedNote = listOfNotes[choice].Id;
            repositoryOfNotes.RemoveNote(removedNote);
        }
        private static void RemoveCategory()
        {
            Console.WriteLine("Выберите удаляемую категорию");
            var choice = int.Parse(Console.ReadLine()) - 1;
            var listOfCategories = repositoryOfCategories.GetAll();
            var removedCategory = listOfCategories[choice].Id;
            repositoryOfCategories.RemoveCategory(removedCategory);
        }
        public static void GetStatistics()
        {
            var statistics = repositoryOfNotes.GetNotes();
            for(int i = 0; i < statistics.Count; i++) 
            {
                var note = statistics[i];
                var category = repositoryOfCategories.GetByID(note.Category);

                Console.WriteLine($"{i + 1}. {category.Name} {note.Amount} рублей {note.Date}\n");
            }
        }

        public static void CreateANewNote()
        {
            Note note = CreateNote();
            SetCategory(note);
            Console.WriteLine("Выведите сумму:\n");
            note.Amount = decimal.Parse(Console.ReadLine());

            repositoryOfNotes.Save(note);
        }

        private static void SetCategory(Note note)
        {
            Console.WriteLine("Выберите катеогорию:");
            Console.WriteLine("n - Создать категорию\n");
            ShowCategories();

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
        }
        public static void ShowCategories()
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
            foreach (var x in list) res += x.Name.ToString() + "\n";
            return res;
        }

        public static void CreateACategory(Note note)
        {
            Console.WriteLine("Введите имя категории");
            var name = Console.ReadLine();
            var cat = new Category() { Name = name };
            var idCategory = repositoryOfCategories.SaveCategory(cat);
            note.Category = idCategory;
        }
        public static Note CreateNote()
        {
            return new Note() { Date = DateTime.Now };
        }
    }
}
