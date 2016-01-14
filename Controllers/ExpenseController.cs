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
    public class ExpenseController : BaseController
    {
        //
        // GET: /Expense/

        public ActionResult Index()
        {
            return View();
        }

        [ExpenseAuthorize]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(FormCollection form)
        {

            return View();
        }

        [HttpPost]
        [ExpenseAuthorize]
        public ActionResult Save(FormCollection form)
        {
            List<string> names, dates, descriptions, costs;

            //Get values
            names = new List<string>(form.GetValues("name[]"));
            dates = new List<string>(form.GetValues("date[]"));
            descriptions = new List<string>(form.GetValues("description[]"));
            costs = new List<string>(form.GetValues("cost[]"));

            string formName = string.Empty;
            //Create new form

            if (!string.IsNullOrEmpty(form["formName"]))
            {
               formName = form["formName"];
            }

                 // TODO: validation
                double totalCost = costs.Sum(c => double.Parse(c));
       
            Models.Form newForm = new Models.Form();
            newForm = FormHelper.Create(formName,totalCost);

          
           

            
            using (ExpenseEntities db = new ExpenseEntities())
            {
                Models.Expense expense;
                for (int i = 0; i < names.Count; i++)
                {
                    
                    expense = new Models.Expense();
                    expense.Id = Guid.NewGuid();
                    expense.Name = names[i];
                    expense.Date = DateTime.Parse(dates[i]);
                    expense.Description = descriptions[i];
                    expense.Cost = int.Parse(costs[i]);
                    expense.FormId = newForm.Id;
                    expense.StateId = newForm.StateId;
                    db.Expenses.Add(expense);
                }
                          
                db.SaveChanges();
            }
           



            return RedirectToAction("New","Expense");
        }

       
    }
}
