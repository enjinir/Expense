using Expense.Helpers;
using Expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Expense.Controllers
{
    public class ExpenseApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<CrmExpenseModel> Get()
        {
            List<CrmExpenseModel> model = new List<CrmExpenseModel>();
            
                var client = new CashFlowIntegrationService.CashFlowIntegrationServiceClient();
                var expenses = client.GetExpenses();

                foreach (var e in expenses)
                {
                    CrmExpenseModel crmExpense = new CrmExpenseModel()
                    {
                        Cost = e.Cost,
                        Source = "Crm"


                    };
                    model.Add(crmExpense);
                }

                ExpenseEntities db = new ExpenseEntities();

                foreach (Models.Form f in db.Forms)
                {
                    CrmExpenseModel expenseAppExpense = new CrmExpenseModel()
                    {
                        Cost = f.Total,
                        Source = "ExpenseApp"

                    };
                    model.Add(expenseAppExpense);

                }
            


            
            return model;
        }

       

        
    }
}