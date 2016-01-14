using Expense.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expense.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        [ExpenseAuthorize]
        public ActionResult Index()
        {
            ViewBag.Username = SessionManager.Get(SessionManager.Keys.Username);
            ViewBag.FullName = SessionManager.Get(SessionManager.Keys.FullName);
            ViewBag.RoleName = SessionManager.Get(SessionManager.Keys.RoleName);
            return View();
        }

    }
}
