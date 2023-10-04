using MyBudget.models;
using MyBudget.services;
using System;
using System.Collections.Generic;

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
                    "3 - Удалить запись");

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

                    default:
                        Console.WriteLine("Введите команду из списка");
                        break;
                }
            }
        }

        public static void RemoveNote()
        {
            Console.WriteLine("Выберите удаляемый элемент");
            var choice = int.Parse(Console.ReadLine()) - 1;
            var listOfNotes = repositoryOfNotes.GetNotes();
            var removedNote = listOfNotes[choice].Id;
            repositoryOfNotes.Remove(removedNote);
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

            Console.WriteLine("Выведите сумму:\n");
            note.Amount = decimal.Parse(Console.ReadLine());

            repositoryOfNotes.Save(note);
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
