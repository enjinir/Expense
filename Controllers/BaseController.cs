using Expense.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expense.Controllers
{
    public class BaseController : Controller
    {
        protected Menu Menu { get; set; }

        public BaseController()
        {
            InitializeMenu();
            
        }

        private void InitializeMenu()
        {
            
            Menu = new Menu();

            if (SessionManager.Check("LoggedIn"))
            {

                //Admin
                if ( (int)(SessionManager.Get(SessionManager.Keys.AuthorizeLevel)) == AuthorizeLevels.Administrator)
                {
                    Menu.Add(new MenuItem("Home", "Index", "Home"));
                    Menu.Add(new MenuItem("Forms", "List","Form"));
                    Menu.Add(new MenuItem("Add Expense", "New", "Expense"));
                    Menu.Add(new MenuItem("Approved", "ApprovedList", "Form"));
                    Menu.Add(new MenuItem("Rejected", "RejectList", "Form"));
                    Menu.Add(new MenuItem("Paid", "PaidList", "Form"));


                }

                //User
                if ((int)(SessionManager.Get(SessionManager.Keys.AuthorizeLevel)) == AuthorizeLevels.User)
                {
                    Menu.Add(new MenuItem("Home", "Index", "Home"));
                    Menu.Add(new MenuItem("Add Expense", "New", "Expense", null));
                    Menu.Add(new MenuItem("Forms", "List", "Form"));
                    Menu.Add(new MenuItem("Approved", "ApprovedList", "Form"));
                    Menu.Add(new MenuItem("Rejected", "RejectList", "Form"));
                    Menu.Add(new MenuItem("Paid", "PaidList", "Form"));



                }
                //Accountant
                if ((int)(SessionManager.Get(SessionManager.Keys.AuthorizeLevel)) == AuthorizeLevels.Accountant)
                {
                    Menu.Add(new MenuItem("Home", "Index", "Home"));
                    Menu.Add(new MenuItem("Forms", "ApprovedList", "Form"));
                    Menu.Add(new MenuItem("Paid", "PaidList", "Form"));

                }

                //Manager
                if ((int)(SessionManager.Get(SessionManager.Keys.AuthorizeLevel)) == AuthorizeLevels.Manager)
                {
                    Menu.Add(new MenuItem("Home", "Index", "Home"));
                    Menu.Add(new MenuItem("Forms", "List", "Form"));
                    Menu.Add(new MenuItem("Approved", "ApprovedList", "Form"));
                    Menu.Add(new MenuItem("Rejected", "RejectList", "Form"));
                    Menu.Add(new MenuItem("Paid", "PaidList", "Form"));


                }
            }


            ViewBag.Menu = this.Menu;
        }

    }
}
