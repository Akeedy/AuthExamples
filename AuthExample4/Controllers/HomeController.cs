using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AuthExample4.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AuthExample4.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet("Login")]
    public IActionResult Login(string returnUrl)
    {
        ViewData["returnUrl"] = returnUrl;
        return View();

    }
    [HttpPost("Login")]
    public async Task<IActionResult> Validate(string username, string password, string returnUrl)
    {
        ViewData["returnUrl"]=returnUrl;
        if (username == "bob" && password == "12345")
        {

            var claims = new List<Claim>(){
             new Claim("username",username),
             new Claim("password",password),
             new Claim(ClaimTypes.Role,"Admin"),
             new Claim(ClaimTypes.Name,"Bob"),
             new Claim(ClaimTypes.NameIdentifier,"Bob-12345"),
           };
            var claimIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(claimPrincipal);
            return Redirect(returnUrl);
        }
        TempData["Error"]="Error. Username or password is invalid";
        return View("Login");
    }
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Secured()
    {
        var idToken=await HttpContext.GetTokenAsync("id_token");
        return View();
    }

    [HttpGet("Denied")]
    public IActionResult Denied(){
        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();
         return Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7267");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
