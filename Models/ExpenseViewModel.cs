using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense.Models
{
    public class ExpenseViewModel
    {
        public string AuthLevel { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public Guid FormId { get; set; }
    }
}