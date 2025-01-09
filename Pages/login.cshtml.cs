using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using e_portfolio.Helpers;

public class LoginModel : PageModel
{
    public string? ErrorMessage { get; private set; }

    public void OnPost(string username, string password)
    {
        var xmlHelper = new XmlHelper();
        var user = xmlHelper.GetUserByUsernameAndPassword(username, password);

        if (user != null)
        {
            HttpContext.Session.SetInt32("UserId", user.Id); // Kullanıcı ID'sini Session'a kaydet
            Response.Redirect("/Index");
        }
        else
        {
            ErrorMessage = "Invalid username or password.";
        }
    }
}
