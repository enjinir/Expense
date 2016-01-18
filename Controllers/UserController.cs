using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Expense.Models;
using Expense.Helpers;

namespace Expense.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username , string password)
        {
            ExpenseEntities db = new ExpenseEntities();
            User user = new User();
            user = (User)db.Users.Where(u=> u.Username.Equals(username)&& u.Password.Equals(password)).FirstOrDefault();
            if (user != null)
            {
                SessionManager.Register(SessionManager.Keys.UserId, user.Id);
                SessionManager.Register(SessionManager.Keys.FullName, user.FirstName + " " + user.LastName);
                SessionManager.Register(SessionManager.Keys.Username, user.Username);
                SessionManager.Register(SessionManager.Keys.RoleName, user.Role.Name);
                SessionManager.Register(SessionManager.Keys.LoggedIn, true);
                SessionManager.Register(SessionManager.Keys.AuthorizeLevel, user.Role.AuthorizeLevel);

                return RedirectToAction("Index","Home");
            }

            SessionManager.Register(SessionManager.Keys.LoggedIn, null);
            

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

    }
}
