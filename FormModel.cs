using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense
{
    public class FormModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid OwnerId { get; set; }
        public string Description { get; set; }
        public string ManagerDescription { get; set; }
        public int Total { get; set; }
        public Guid StateId { get; set; }
    }
}