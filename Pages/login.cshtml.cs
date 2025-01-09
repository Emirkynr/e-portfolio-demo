using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using e_portfolio.Helpers;
using e_portfolio.Pages;


public class LoginModel : BasePageModel
{
    public string? ErrorMessage { get; private set; }

    public void OnPost(string username, string password)
    {
        var XmlHelper = new XmlHelper();
        Console.WriteLine("deneme" + username + password);
        var user = XmlHelper.GetUserByUsernameAndPassword(username, password);

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
