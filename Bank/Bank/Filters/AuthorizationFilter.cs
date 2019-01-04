using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Filters
{
    public class AuthorizationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Controller controller = context.Controller as Controller;

            if (!string.IsNullOrEmpty(context.HttpContext.Session.GetString("Role")))
            {
                controller.ViewBag.Role = context.HttpContext.Session.GetString("Role");
            }
            else
            {
                controller.ViewBag.Role = "None";
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //after
        }
    }
}
