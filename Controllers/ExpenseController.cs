using Expense.Helpers;
using Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Expense.Models;

namespace Expense.Controllers
{
    public class ExpenseController : Controller
    {
        //
        // GET: /Expense/

        public ActionResult Index()
        {
            return View();
        }

        [ExpenseAuthorize]
        public ActionResult Create()
        {
            FormModel form = new FormModel();
            form = FormHelper.Create();
            return View(form);
        }


        [HttpPost]
        public ActionResult Create(Models.Expense expense)
        {
            
         
            return View();
        }
    }
}
