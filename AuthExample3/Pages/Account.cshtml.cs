using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AuthExample3.Pages
{
    public class AccountModel : PageModel
    {
        [BindProperty]
        public Credentail Credentail { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (Credentail.UserName == "admin" && Credentail.Password == "orhan")
            {
                //Create security context
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name,"admin"),
                    new Claim(ClaimTypes.Email,"admin@gmail.com"),
                    new Claim("Department","HR"),
                    new Claim("Admin",""),
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var cliamPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", cliamPrincipal);
                return RedirectToPage("/Index");
            }
            return Page();
        }

    }

    public class Credentail
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}