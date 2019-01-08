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
using Bank.Filters;

namespace Bank.Controllers
{
    [UserFilter]
    public class PaymentController : Controller
    {
        private readonly BankContext _context;

        public PaymentController(BankContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Payment));
        }

        public async Task<IActionResult> Payment()
        {
            ViewBag.Templates = await GetTemplates();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(Payment payment)
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

            return View(await _context.Payment.Where(e => e.UserId == user.Id).ToListAsync());
        }

        public async Task<IActionResult> PaymentTemplate(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Payment));

            var template = await _context.Template.FindAsync(id);
            if(template == null) return RedirectToAction(nameof(Payment));
            
            ViewBag.Templates = await GetTemplates();

            return View("Payment", new Payment(template));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        private async Task<List<Template>> GetTemplates()
        {
            User user = null;
            string userId = HttpContext.Session.GetString("UserId");
            SessionHandler.GetUser(userId, out user);

            return await _context.Template.Where(e => e.UserId == user.Id).ToListAsync();
        }
    }
}
