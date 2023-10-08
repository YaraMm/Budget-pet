using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.models
{
    internal class Note
    {
        public Guid Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid Id { get; set; }

        public Note Copy()
        {
            return new Note
            {
                Category = Category,
                Amount = Amount,
                Date = Date,
                Id = Id
            };
        }
    }
}
