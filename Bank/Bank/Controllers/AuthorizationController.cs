using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Login([Bind("Login")] int login, [Bind("Pin")] int Pin)
        {
            try
            {
                User usr = await _context.User.FirstOrDefaultAsync(u => u.Login == login);

                if (usr == null || usr.Pin != Pin)
                {
                    ViewBag.Login = login;
                    ViewBag.ErrMsg = "Wrong login or pin!";
                    return View();
                }

                HttpContext.Session.SetString("Role", usr.Role.ToString());
                return Redirect("/Home/Index");
            }
            catch
            {
                return Redirect("/Home/Error");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.SetString("Role", null);
            ViewBag.Role = null;

            return Redirect("/Home/Index");
        }

        public IActionResult Unauth()
        {
            return View();
        }

    }
}