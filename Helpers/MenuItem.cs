using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense.Helpers
{
    public class MenuItem
    {
        string actionName, controllerName, text;
        object routeValues;

        public MenuItem(string text)
        {
            this.text = text;
            actionName = "Index";
            controllerName = "Home";    
        }
        public MenuItem(string text, string actionName , string controllerName)
        {
            this.text = text;
            this.actionName = actionName;
            this.controllerName = controllerName;
        }
        public MenuItem(string text, string actionName , string controllerName , object routeValues)
        {
            this.text = text;
            this.actionName = actionName;
            this.controllerName = controllerName;
            this.routeValues = routeValues;
        }

        public string Text 
        {
            get { return text; }
            set { text = value; }
        }
        public string ActionName
        {
            get { return actionName; }
            set { actionName = value; }
        }
        public string ControllerName
        {
            get { return controllerName; }
            set { controllerName = value; }
        }
        public object RouteValues
        { 
            get { return routeValues;}
            set { routeValues = value; }
        }
    }
}