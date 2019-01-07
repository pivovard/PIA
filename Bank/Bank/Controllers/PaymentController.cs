using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Bank.Data;

namespace Bank.Controllers
{
    public class PaymentController : Controller
    {
        private readonly BankContext _context;

        public PaymentController(BankContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Payment payment)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                string userId = HttpContext.Session.GetString("UserId");
                SessionHandler.GetUser(userId, out user);

                try
                {
                    if (await _context.MakePayment(user, payment))
                    {
                        return RedirectToAction(nameof(PaymentList));
                    }
                    else
                    {
                        ViewBag.Insufficient = "Insufficient money.";
                        return View(payment);
                    }
                }
                catch
                {
                    if (!UserExists(user.Id))
                    {
                        return Redirect("/User/UNotFound");
                    }
                    else
                    {
                        return Redirect("/Home/Error");
                    }
                }
            }

            ViewBag.ErrMsg = "Values are not valid.";
            return View(payment);
        }

        public async Task<IActionResult> PaymentList()
        {
            User user = null;
            string userId = HttpContext.Session.GetString("UserId");
            SessionHandler.GetUser(userId, out user);

            return View(await _context.Payment.Where(p => p.UserId == user.Id).ToListAsync());
        }

        public async Task<IActionResult> TemplateList()
        {
            User user = null;
            string userId = HttpContext.Session.GetString("UserId");
            SessionHandler.GetUser(userId, out user);

            return View(await _context.Template.Where(t => t.UserId == user.Id).ToListAsync());
        }

        public IActionResult AddTemplate()
        {
            return View();
        }

        public async Task<IActionResult> EditTemplate(int? id)
        {
            if (id == null) return View();

            var temp = await _context.Template.FindAsync(id);
            if (temp == null) return View();
            
            return View("AddTemplate", temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTemplate(int id,  Template template)
        {
            if (ModelState.IsValid)
            {
                var temp = await _context.Template.FindAsync(id);

                if (temp == null)
                {
                    if (!_context.IsTemplateNameUnique(template.Name))
                    {
                        
                        ViewBag.ErrName = "Template name must be unique.";
                        return View(template);
                    }

                    User user = null;
                    string userId = HttpContext.Session.GetString("UserId");
                    SessionHandler.GetUser(userId, out user);

                    template.UserId = user.Id;

                    try
                    {
                        _context.Add(template);
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        return Redirect("/Home/Error");
                    }
                }
                else
                {
                    if (!_context.IsTemplateNameUnique(template.Name) && temp.Name != template.Name)
                    {

                        ViewBag.ErrName = "Template name must be unique.";
                        return View(template);
                    }

                    try
                    {
                        _context.Update(template);
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        return Redirect("/Home/Error");
                    }
                }
            }

            return View(template);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
