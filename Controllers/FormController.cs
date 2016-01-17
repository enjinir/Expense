using Expense.Helpers;
using Expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expense.Controllers
{
    public class FormController : BaseController
    {
        //
        // GET: /Form/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            IEnumerable<Form> forms = null;
            User user = new User();

            ExpenseEntities db = new ExpenseEntities();
                Guid userId = Guid.Parse(SessionManager.Get(SessionManager.Keys.UserId).ToString());

                //Admin can see all forms
                if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Administrator)
                {
                    forms = db.Forms.ToList();
                   
                }

                //Manager cans see the forms which ones manager is the current manager
                if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Manager)
                {
                    forms = db.Forms
                                .Where(f => (f.OwnerId == userId
                                            || f.User.ManagerId == userId) 
                                            && f.State.Name != "Paid")
                                            .ToList();
                }

                //User can see only current users forms
                if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.User)
                {
                    forms = db.Forms.Where(f => f.OwnerId == userId).ToList();
                }

                //Accountat can see only approved forms.
                if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Accountant)
                {
                    forms = db.Forms
                                .Where(f => f.State.Name == "Approved")
                                .ToList();
                }
                                            
                


            return View(forms);
        }

        public ActionResult ApprovedList()
        {
            IEnumerable<Form> forms = null;
            ExpenseEntities db = new ExpenseEntities();

            State state = db.States.Where(s=> s.Name == "Approved").FirstOrDefault();
            Guid userId = Guid.Parse(SessionManager.Get(SessionManager.Keys.UserId).ToString());

            
            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Accountant)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id)).ToList();
            }

            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.User)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id) 
                                       && f.OwnerId == userId).ToList();
            }
            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Administrator)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id)).ToList();
            }
            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Manager)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id) && 
                                        f.User.ManagerId == userId).ToList();
            }


            
            return View(forms);

        }

        public ActionResult PaidList()
        {
            IEnumerable<Form> forms = null;
            ExpenseEntities db = new ExpenseEntities();

            State state = db.States.Where(s => s.Name == "Paid").FirstOrDefault();
            Guid userId = Guid.Parse(SessionManager.Get(SessionManager.Keys.UserId).ToString());


            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Accountant)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id)).ToList();
            }

            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.User)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id)
                                       && f.OwnerId == userId).ToList();
            }
            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Administrator)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id)).ToList();
            }
            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Manager)
            {
                forms = db.Forms.Where(f => f.StateId.Equals(state.Id) &&
                                        f.User.ManagerId == userId).ToList();
            }



            return View(forms);
        
        }

        [ExpenseAuthorize]
        public ActionResult Edit(Models.Form form)
        {
            return View(form);
        }

        public ActionResult Approve(Guid id)
        {
            Models.Form form = new Models.Form();
            ExpenseEntities db = new ExpenseEntities();
            form = db.Forms.Where(f => f.Id == id).FirstOrDefault();      
            State state = db.States.Where(s => s.Name == "Approved").FirstOrDefault();
            form.StateId = state.Id;
            List<Models.Expense> expenses = new List<Models.Expense>();
            expenses = db.Expenses.Where(e => e.FormId == id).ToList();
            foreach (Models.Expense e in expenses)
            {
                e.StateId = state.Id;
            }   
            db.SaveChanges();

            return RedirectToAction("List","Form");
        }

        public ActionResult Pay(Guid id)
        {
            Models.Form form = new Models.Form();
            ExpenseEntities db = new ExpenseEntities();
            form = db.Forms.Where(f => f.Id == id).FirstOrDefault();
            State state = db.States.Where(s => s.Name == "Paid").FirstOrDefault();
            form.StateId = state.Id;
            List<Models.Expense> expenses = new List<Models.Expense>();
            expenses = db.Expenses.Where(e => e.FormId == id).ToList();
            foreach (Models.Expense e in expenses)
            {
                e.StateId = state.Id;
            }
            db.SaveChanges();

            return RedirectToAction("List", "Form");
        }

        public ActionResult Reject(Guid id)
        {
            Expense.Models.Form form = new Expense.Models.Form();
            ExpenseEntities db = new ExpenseEntities();
            form = db.Forms.Where(f=> f.Id == id).FirstOrDefault();
      
            return View(form);
        }

        [HttpPost]
          public ActionResult Reject(Guid id, string rejectText)
        {
            Expense.Models.Form form = new Expense.Models.Form();
            ExpenseEntities db = new ExpenseEntities();
            form = db.Forms.Where(f=> f.Id == id).FirstOrDefault();
            IEnumerable<Models.Expense> expenses = db.Expenses.Where(e => e.FormId == id).ToList();
            form.ManagerDescription = rejectText;
            State state = db.States.Where(s=> s.Name == "Reject").FirstOrDefault();
            foreach (Models.Expense e in expenses)
            {
                if (e.State.Name != "Approved")
                {
                    e.StateId = state.Id;
                }
            
            }
            form.StateId = state.Id;
            db.SaveChanges();

            return RedirectToAction("List","Form");
        }

        public ActionResult RejectList()
        {
            IEnumerable<Form> forms = null;
            ExpenseEntities db = new ExpenseEntities();

            State state = db.States.Where(s => s.Name == "Reject").FirstOrDefault();
            Guid userId = Guid.Parse(SessionManager.Get(SessionManager.Keys.UserId).ToString());

            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Administrator)
            {
                forms = db.Forms.Where(f => f.State.Id == state.Id).ToList();
            }

            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.User)
            {
                forms = db.Forms.Where(f => f.State.Id == state.Id
                                        && f.OwnerId == userId )
                                        .ToList();
            }
            if ((int)SessionManager.Get(SessionManager.Keys.AuthorizeLevel) == AuthorizeLevels.Manager)
            {
                forms = db.Forms.Where(f => f.State.Id == state.Id
                                       && f.User.ManagerId == userId)
                                       .ToList();
            }


            
            return View(forms);

        }

       

       


    }
}
