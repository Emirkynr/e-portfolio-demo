using Microsoft.AspNetCore.Mvc.RazorPages;
using e_portfolio.Helpers;
using e_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_portfolio.Pages
{
    public class PersonalInfoModel : BasePageModel
    {
        private readonly XmlHelper _xmlHelper;

        public PersonalInfoModel(XmlHelper xmlHelper)
        {
            _xmlHelper = xmlHelper;
        }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Surname { get; set; }

        [BindProperty]
        public string? Email { get; set; }
        public string? GitHub { get; set; }

        [BindProperty]
        public string? LinkedIn { get; set; }


        public void OnGet()
        {
            // DisplayUser is set in BasePageModel
            if (DisplayUser != null)
            {
                Name = DisplayUser.Name;
                Surname = DisplayUser.Surname;
                GitHub = DisplayUser.GitHub;
                LinkedIn = DisplayUser.LinkedIn;
                Email = DisplayUser.Email;
            }
        }

        public IActionResult OnPost()
        {
            if (DisplayUser != null)
            {
                DisplayUser.Name = Name;
                DisplayUser.Surname = Surname;
                DisplayUser.GitHub = GitHub;
                DisplayUser.LinkedIn = LinkedIn;
                DisplayUser.Email = Email;

                _xmlHelper.UpdateUser(DisplayUser);
            }

            return RedirectToPage();
        }
    }
}