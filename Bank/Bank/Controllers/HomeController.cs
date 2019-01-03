using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Microsoft.AspNetCore.Http;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Name"))) ViewBag.Role = "Admin";
            if (HttpContext.Session.GetString("Name") != null)
            {
                //ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Log = false;
            }
            
            return View();
        }

        public IActionResult About()
        {
            HttpContext.Session.SetString("Name", "Jmeno");
            if (HttpContext.Session.GetString("Name") != null)
            {
                ViewBag.Log = true;
            }
            else
            {
                ViewBag.Log = false;
            }

            return View();
        }

        public IActionResult Contact()
        {
            HttpContext.Session.Remove("Name");
            if (HttpContext.Session.GetString("Name") != null)
            {
                ViewBag.Log = true;
            }
            else
            {
                ViewBag.Log = false;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Unauth()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
