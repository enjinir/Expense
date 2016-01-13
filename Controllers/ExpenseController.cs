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
            ExpenseEntities db = new ExpenseEntities();
            Form form = (Form)db.Forms.Where(f => f.Id == expense.FormId );
           
            ///Find form whichs id = expense.ownerid and use it ..
            ///blah blah

            
         
            return View();
        }
    }
}
