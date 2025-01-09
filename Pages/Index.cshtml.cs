using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using e_portfolio.Helpers;
using e_portfolio.Models;   

namespace e_portfolio.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public User? DisplayUser { get; private set; }

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
            DisplayUser = xmlHelper.GetUserById(userId.Value); // ID'ye göre kullanıcı bilgilerini al
        }
        else
        {
            // Kullanıcı giriş yapmadıysa, login sayfasına yönlendirin
            Response.Redirect("/Login");
        }
    }
}
