using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using e_portfolio.Helpers;
using e_portfolio.Pages;

namespace e_portfolio.Pages
{
    public class LoginModel : BasePageModel
    {
        private readonly XmlHelper _xmlHelper;

        public LoginModel(XmlHelper xmlHelper)
        {
            _xmlHelper = xmlHelper;
        }

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username or password cannot be empty.";
                return Page(); // Return the page with the error message
            }

            var user = _xmlHelper.GetUserByUsernameAndPassword(Username, Password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id); // Save user ID to session
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page(); // Return the page with the error message
            }
        }
    }
}