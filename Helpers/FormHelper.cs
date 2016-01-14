using Expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense.Helpers
{
    public class FormHelper
    {
        public static Models.Form Create(string name, double totalCost)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = SessionManager.Get(SessionManager.Keys.FullName).ToString() + " , " + DateTime.Now.ToString();
            }
            ExpenseEntities db = new ExpenseEntities();
            Models.Form form = new Models.Form();
            form.Id = Guid.NewGuid();
            form.Date = DateTime.Now;
            form.Description = SessionManager.Get(SessionManager.Keys.FullName).ToString() + " Adlı kullanıcının"  + DateTime.Now.ToString() + "Tarihli Formu";
            form.Name = name;
            form.OwnerId = Guid.Parse((SessionManager.Get(SessionManager.Keys.UserId).ToString()));
            State state = (State)db.States.Where(s => s.Name == "Pending").FirstOrDefault();
            form.StateId = state.Id;
            form.Total = (int)totalCost;
            db.Forms.Add(form);
            db.SaveChanges();
          
           

            return form;
        }

    }
}