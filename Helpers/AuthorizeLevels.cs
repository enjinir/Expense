using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense.Helpers
{
    public static class AuthorizeLevels
    {
        public const int Administrator = 1000;
        public const int User = 10;
        public const int Accountant = 20;
        public const int Manager = 30;  
    }
}