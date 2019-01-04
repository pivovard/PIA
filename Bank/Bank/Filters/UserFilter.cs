using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Filters
{
    public class UserFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Controller controller = context.Controller as Controller;

            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("Role")) || controller.ViewBag.Role == "None")
            {
                context.Result = new ContentResult()
                {
                    Content = "Unauthorized access!"
                };
                controller.Response.Redirect("/Authorization/Login");
            }
            else if (!context.HttpContext.Session.GetString("Role").Equals("User") || !controller.ViewBag.Role != "User")
            {
                context.Result = new ContentResult()
                {
                    Content = "Unauthorized access!"
                };
                controller.Response.Redirect("/Authorization/Unauth");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
