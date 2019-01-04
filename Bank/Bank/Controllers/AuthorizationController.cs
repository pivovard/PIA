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

        public async Task<IActionResult> Login()
        {
            User user = new User();
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            catch { }
            
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                if (!ModelState.IsValid) return View(user);

                User usr = await _context.User.FirstOrDefaultAsync(u => u.Login == user.Login);

                if (usr == null) return View(user);
                if (usr.Pin != user.Pin) return View(user);

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

            return View();
        }

        public IActionResult Unauth()
        {
            return View();
        }



        // GET: Authorization
        public ActionResult Index()
        {
            return View();
        }

        // GET: Authorization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Authorization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authorization/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Authorization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Authorization/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Authorization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Authorization/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}