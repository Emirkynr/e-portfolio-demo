using Microsoft.AspNetCore.Mvc.RazorPages;
using e_portfolio.Helpers;
using e_portfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace e_portfolio.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly XmlHelper _xmlHelper;

        public CreateUserModel(XmlHelper xmlHelper)
        {
            _xmlHelper = xmlHelper;
        }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Surname { get; set; }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname))
            {
                var newUser = new User
                {
                    Id = GetNewUserId(),
                    Name = Name,
                    Surname = Surname
                };

                _xmlHelper.CreateUser(newUser);
                return RedirectToPage("/Index");
            }

            return Page();
        }

        private int GetNewUserId()
        {
            // Implement logic to generate a new unique user ID
            // For simplicity, you can use a static counter or read the last ID from the XML file
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