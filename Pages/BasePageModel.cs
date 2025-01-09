using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Filters;
using e_portfolio.Models;
using e_portfolio.Helpers;

namespace e_portfolio.Pages
{
    public class BasePageModel : PageModel
    {
        public User? DisplayUser { get; set; }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var xmlHelper = new XmlHelper();
                DisplayUser = xmlHelper.GetUserById(userId.Value);
            }
        }
    }
}