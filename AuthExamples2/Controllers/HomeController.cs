using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthExamples.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string username,string password)
        {
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Secure()
        {
            return View(); 
        }
    }
}
