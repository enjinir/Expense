using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expense.Helpers
{

    public class Menu
    {
        private UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

        public List<MenuItem> Items { get; set; }
        
        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public void Add(MenuItem item)
        {
            Items.Add(item);
        }
    }
}