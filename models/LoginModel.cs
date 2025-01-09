using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using e_portfolio.Helpers;
using e_portfolio.Models;
using System.Xml;


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

        [BindProperty]
        public string? RegisterName { get; set; }

        [BindProperty]
        public string? RegisterSurname { get; set; }

        [BindProperty]
        public string? RegisterUsername { get; set; }

        [BindProperty]
        public string? RegisterPassword { get; set; }

        public string? ErrorMessage { get; private set; }
        public string? SuccessMessage { get; private set; }

        public IActionResult OnPostLogin()
        {
            var user = _xmlHelper.GetUserByUsernameAndPassword(Username, Password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }
        }

        public IActionResult OnPostRegister()
        {
            if (!string.IsNullOrEmpty(RegisterName) && !string.IsNullOrEmpty(RegisterSurname) &&
                !string.IsNullOrEmpty(RegisterUsername) && !string.IsNullOrEmpty(RegisterPassword))
            {
                var newUser = new User
                {
                    Id = GetNewUserId(),
                    Name = RegisterName,
                    Surname = RegisterSurname,
                    Username = RegisterUsername,
                    Password = RegisterPassword
                };

                _xmlHelper.CreateUser(newUser);
                SuccessMessage = "KAYIT ISLEMI BASARILI!";
                return Page();
            }

            ErrorMessage = "TUM KUTUCUKLARI DOLDURUNUZ!";
            return Page();
        }

        private int GetNewUserId()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");
            var userNodes = xmlDoc.SelectNodes("/Users/User");
            int maxId = 0;
            if (userNodes != null)
            {
                foreach (XmlNode userNode in userNodes)
                {
                    int id = int.Parse(userNode.Attributes["ID"].Value);
                    if (id > maxId)
                    {
                        maxId = id;
                    }
                }
            }
            return maxId + 1;
        }
    }
}