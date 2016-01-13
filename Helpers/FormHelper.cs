using Expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense.Helpers
{
    public class FormHelper
    {
        public static FormModel Create()
        { 
            ExpenseEntities db = new ExpenseEntities();
            FormModel form = new FormModel();
            form.Id = Guid.NewGuid();
            form.Date = DateTime.Now;
            form.Description = SessionManager.Get(SessionManager.Keys.FullName).ToString() + " Adlı kullanıcının"  + DateTime.Now.ToString() + "Tarihli Formu";
            form.Name = SessionManager.Get(SessionManager.Keys.FullName).ToString() + " , " + DateTime.Now.ToString();
            form.OwnerId = Guid.Parse((SessionManager.Get(SessionManager.Keys.UserId).ToString()));
            State state = (State)db.States.Where(s => s.Name == "Beklemede").FirstOrDefault();
            form.StateId = state.Id;
            form.Total = 0;
            return form;
        }

    }
}