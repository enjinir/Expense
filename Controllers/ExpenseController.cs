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

        [ExpenseAuthorize]
        public ActionResult List(Guid id)
        { 
            ExpenseEntities db = new ExpenseEntities();
            IEnumerable<Expense.Models.Expense> expenses = db.Expenses.Where(e => e.FormId.Equals(id)).AsEnumerable();
            Form form =  db.Forms.Where(f=> f.Id == id).FirstOrDefault();
            return View(
                new ExpenseViewModel()
                {
                    AuthLevel = (SessionManager.Get(SessionManager.Keys.AuthorizeLevel) ?? string.Empty).ToString(),
                    Expenses = expenses,
                    FormId = id,
                    FormState =form.State.Name
                }
            );       
        }

        [ExpenseAuthorize]
        public ActionResult Approve(Guid id)
        {
            Models.Expense expense = new Models.Expense();
            ExpenseEntities db = new ExpenseEntities();
            expense = db.Expenses.Where(e => e.Id == id).FirstOrDefault();
            State state = db.States.Where(s => s.Name == "Approved").FirstOrDefault();
            expense.StateId = state.Id;
            Guid formId = expense.FormId;
            db.SaveChanges();

            return RedirectToAction("List", "Expense", new {id=formId});
        }

        [ExpenseAuthorize]
        public ActionResult Reject(Guid id)
        {
            Models.Expense expense = new Models.Expense();
            ExpenseEntities db = new ExpenseEntities();
            expense = db.Expenses.Where(e => e.Id == id).FirstOrDefault();
            State state = db.States.Where(s => s.Name == "Reject").FirstOrDefault();
            expense.StateId = state.Id;
            Guid formId = expense.FormId;
            db.SaveChanges();

            return RedirectToAction("List", "Expense", new { id = formId });
        }

       


      

       
    }
}
