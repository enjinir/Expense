using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense.Helpers
{
    public class SessionManager
    {
      
            public static void Register(string key, object obj)
            {
                System.Web.HttpContext.Current.Session[key] = obj;
            }


            public static void Delete(string key)
            {
                System.Web.HttpContext.Current.Session[key] = null;
            }

            public static bool Check(string key)
            {
                if (System.Web.HttpContext.Current.Session[key] != null)
                    return true;
                else
                    return false;
            }
          

            public static object Get(string key)
            {
                if (Check(key))
                    return System.Web.HttpContext.Current.Session[key];
                else
                    return null;
            }
           

            public static class Keys
            {
                public const string UserId = "UserId";
                public const string Username = "Username";
                public const string Password = "Password";
                public const string RoleName = "RoleName";
                public const string FullName = "FullName";
                public const string LoggedIn = "LoggedIn";
                public const string AuthorizeLevel = "AuthorizeLevel";
            }
        }
}
