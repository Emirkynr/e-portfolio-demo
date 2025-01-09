using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using e_portfolio.Helpers;
using e_portfolio.Models;

namespace e_portfolio.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var xmlHelper = new XmlHelper();
                DisplayUser = xmlHelper.GetUserById(userId.Value);
            }
            else
            {
                Response.Redirect("/Login");
            }
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}