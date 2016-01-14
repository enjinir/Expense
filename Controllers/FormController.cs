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
        public ActionResult Edit(Models.Form form)
        {
            return View(form);
        }

    }
}
