using Microsoft.AspNetCore.Mvc.RazorPages;
using e_portfolio.Helpers;
using e_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_portfolio.Pages
{
    public class EducationModel : BasePageModel
    {
        private readonly XmlHelper _xmlHelper;

        public EducationModel(XmlHelper xmlHelper)
        {
            _xmlHelper = xmlHelper;
        }

        [BindProperty]
        public string? okulAdi { get; set; }

        [BindProperty]
        public string? bolumAdi { get; set; }

        [BindProperty]
        public string? egitimDüzeyi { get; set; }

        [BindProperty]
        public string? egitimYillari { get; set; }

        public void OnGet()
        {
            // DisplayUser is set in BasePageModel
            if (DisplayUser != null)
            {
                okulAdi = DisplayUser.okulAdi;
                bolumAdi = DisplayUser.bolumAdi;
                egitimDüzeyi = DisplayUser.egitimDüzeyi;
                egitimYillari = DisplayUser.egitimYillari;
            }
        }

        public IActionResult OnPost()
        {
            if (DisplayUser != null)
            {
                DisplayUser.okulAdi = okulAdi;
                DisplayUser.bolumAdi = bolumAdi;
                DisplayUser.egitimDüzeyi = egitimDüzeyi;
                DisplayUser.egitimYillari = egitimYillari;

                _xmlHelper.UpdateUser(DisplayUser);
            }

            return RedirectToPage();
        }
    }
}