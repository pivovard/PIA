using Bank.Data;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Filters
{
    public class AdminFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            User u = null;

            string userId = context.HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId) || !SessionHandler.GetUser(userId, out u))
            {
                context.Result = new ContentResult();
                controller.Response.Redirect("/Authorization/Login");
            }
            else if (u.Role != Role.Admin)
            {
                context.Result = new ContentResult();
                controller.Response.Redirect("/Authorization/Unauth");
            }
        }
    }
}
