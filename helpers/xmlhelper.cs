// Helpers/XmlHelper.cs
using System.Xml.Linq;
using e_portfolio.Models;
using System.Linq;

namespace e_portfolio.Helpers
{
    public class XmlHelper
    {
        public User? GetUserById(int id)
        {
            var xdoc = XDocument.Load("users.xml");

            var userElement = xdoc.Descendants("User")
                .FirstOrDefault(u => (int?)u.Element("ID") == id);

            if (userElement == null)
            {
                return null;
            }

            return new User
            {
                ID = int.Parse(userElement.Element("ID")?.Value ?? "0"),
                FirstName = userElement.Element("FirstName")?.Value,
                LastName = userElement.Element("LastName")?.Value,
                Email = userElement.Element("Email")?.Value,
                Password = userElement.Element("Password")?.Value
            };
        }
    }
}
