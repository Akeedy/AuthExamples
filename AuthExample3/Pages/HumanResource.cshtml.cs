using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthExample3.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
