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
        public IActionResult Authenticate()
        {

            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email,"Bob@gmail.com"),
                new Claim("Grandma Says","Bob is asshole"),
            };
        
            var licenceClaims= new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob Ahr+"),
                new Claim("DriverLicense ","A"),
            };
            var grandmaIdentity=new ClaimsIdentity(grandmaClaims,"Grandma Identity");
            var licenseIdentity=new ClaimsIdentity(licenceClaims,"License Identity");


            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity,licenseIdentity});

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Secure()
        {
            return View(); 
        }
    }
}
