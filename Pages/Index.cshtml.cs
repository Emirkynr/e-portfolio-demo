using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using e_portfolio.Helpers;
using e_portfolio.Models;

namespace e_portfolio.Pages;

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
            var XmlHelper = new XmlHelper();
            DisplayUser = XmlHelper.GetUserById(userId.Value); // DisplayUser BasePageModel'den miras alınıyor
        }
        else
        {
            Response.Redirect("/Login");
        }
    }
}
