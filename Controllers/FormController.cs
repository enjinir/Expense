using Expense.Helpers;
using Expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expense.Controllers
{
    public class FormController : Controller
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
            
            using (ExpenseEntities db = new ExpenseEntities())
            {
                Guid userId = Guid.Parse(SessionManager.Get(SessionManager.Keys.UserId).ToString());
                forms = db.Forms
                            .Where(f => f.OwnerId == userId
                                        || f.User.ManagerId == userId)
                            .ToList();

            }

            return View(forms);
        }

        [ExpenseAuthorize]
        public ActionResult Create()
        {

            return View();
        }


        public ActionResult Create()
        {
            Form form = new Form();
            try
            {

                ExpenseEntities db = new ExpenseEntities();
                form.Date = DateTime.Now;
                form.Name = SessionManager.Get(SessionManager.Keys.FullName).ToString() + " , " + DateTime.Now.ToString() ;
                form.Description = SessionManager.Get(SessionManager.Keys.FullName).ToString() + "Adlı Kullanıcının" 
                                   + DateTime.Now.ToString() + "Tarihli Formu.";
                form.Total = 0;
                form.Id = Guid.NewGuid();
                form.Date = DateTime.Now;
                Guid OwnerId = Guid.Parse(SessionManager.Get(SessionManager.Keys.UserId).ToString());
                form.OwnerId = OwnerId;
                State state = (State)db.States.Where(s => s.Name == "Beklemede").FirstOrDefault();
                form.StateId = state.Id;
                db.Forms.Add(form);
                db.SaveChanges();

               // return RedirectToAction("Create", "Expense", form);
                return View();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException exc)
            {
                throw exc;
            }
        }

    }
}
