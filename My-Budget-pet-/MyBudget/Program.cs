using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget
{
    internal class Program
    {
        public class Categories
        {
            string name;
        }

        Categories food = new Categories();
        

        public class Expenses
        {
            Categories name;
            int price;
        }

        public static void MakeExpense(Expenses name, Expenses price)
        {

        }
        static void Main(string[] args)
        {
        }
    }
}
