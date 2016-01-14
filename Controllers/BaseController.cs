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
                //User
                if ((int)(SessionManager.Get(SessionManager.Keys.AuthorizeLevel)) == AuthorizeLevels.Administrator)
                {
                    Menu.Add(new MenuItem("Home", "Index", "Home"));
                }
            }


            ViewBag.Menu = this.Menu;
        }

    }
}
