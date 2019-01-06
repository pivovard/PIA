﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Filters;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bank.Controllers
{
    public class UserController : Controller
    {
        private readonly BankContext _context;

        public UserController(BankContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            User user = null;
            string userId = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userId) && SessionHandler.GetUser(userId, out user))
            {
                return View(user);
            }
            else
            {
                return Redirect("/Authorization/Login");
            }
        }

        public IActionResult Edit()
        {
            User user = null;
            string userId = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userId) && SessionHandler.GetUser(userId, out user))
            {
                return View(user);
            }
            else
            {
                return Redirect("/Authorization/Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BirthNumber,Adress,Email,Phone,AccountNumber,CardNumber,Money,Login,Pin,Role")] User user)
        {
            if (id != user.Id) return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Redirect("/Home/Error");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult ChangePin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePin([Bind("OldPin")] int oldPin, [Bind("NewPin")] int newPin, [Bind("confirmPin")] int confirmPin)
        {
            User user = null;
            string userId = HttpContext.Session.GetString("UserId");
            
            if (!string.IsNullOrEmpty(userId) && SessionHandler.GetUser(userId, out user))
            {
                if (!user.HashPin(oldPin).Equals(user.Pin))
                {
                    ViewBag.ErrMsg = "Wrong old pin!";
                    return View();
                }

                if (newPin != confirmPin)
                {
                    ViewBag.ErrMsg = "Confirm pin does not match.";
                    return View();
                }

                if (newPin < 1000 || newPin > 99999999)
                {
                    ViewBag.ErrMsg = "Pin must be min 4 and max 8 digits long.";
                    return View();
                }

                user.Pin = user.HashPin(newPin);

                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Redirect("/Home/Error");
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return Redirect("/Authorization/Login");
            }
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }
}