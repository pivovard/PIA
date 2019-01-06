using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly BankContext _context;

        public AuthorizationController(BankContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Login")] string login, [Bind("Pin")] int pin)
        {
            try
            {
                User user = await _context.User.FirstOrDefaultAsync(u => u.Login == login);

                if (user == null || !user.HashPin(pin).Equals(user.Pin))
                {
                    ViewBag.Login = login;
                    ViewBag.ErrMsg = "Wrong login or pin!";
                    return View();
                }
                
                HttpContext.Session.SetString("UserId", SessionHandler.NewSession(user));
                if(user.Role == Role.User)
                {
                    return Redirect("/User");
                }
                else
                {
                    return Redirect("/Admin");
                }
            }
            catch
            {
                return Redirect("/Home/Error");
            }
        }

        public IActionResult Logout()
        {
            SessionHandler.DestroySession(HttpContext.Session.GetString("UserId"));
            HttpContext.Session.Clear();
            ViewBag.Role = "None";

            return Redirect("/Home/Index");
        }

        public IActionResult Unauth()
        {
            return View();
        }

    }
}