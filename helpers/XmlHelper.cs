using System.Xml.Linq;
using e_portfolio.Models;
using System.Xml;

namespace e_portfolio.Helpers
{
    public class XmlHelper
    {
        public User? GetUserByUsernameAndPassword(string username, string password)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");

            var userNodes = xmlDoc.SelectNodes("/Users/User");

            if (userNodes != null)
            {
                foreach (XmlNode userNode in userNodes)
                {
                    var usernameNode = userNode.SelectSingleNode("Username");
                    var passwordNode = userNode.SelectSingleNode("Password");

                    if (usernameNode != null && passwordNode != null &&
                        usernameNode.InnerText == username &&
                        passwordNode.InnerText == password)
                    {
                        return new User
                        {
                            Id = int.Parse(userNode.Attributes["ID"].Value),
                            Name = userNode.SelectSingleNode("FirstName")?.InnerText,
                            Surname = userNode.SelectSingleNode("LastName")?.InnerText,
                            Email = userNode.SelectSingleNode("Email")?.InnerText,
                            Username = usernameNode.InnerText,
                            Password = passwordNode.InnerText
                        };
                    }
                }
            }

            return null;
        }

        public User? GetUserById(int id)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");

            var userNode = xmlDoc.SelectSingleNode($"/Users/User[@ID='{id}']");

            if (userNode != null)
            {
                return new User
                {
                    Id = id,
                    Name = userNode.SelectSingleNode("FirstName")?.InnerText,
                    Surname = userNode.SelectSingleNode("LastName")?.InnerText,
                    Email = userNode.SelectSingleNode("Email")?.InnerText,
                    Username = userNode.SelectSingleNode("Username")?.InnerText,
                    Password = userNode.SelectSingleNode("Password")?.InnerText
                };
            }

            return null;
        }
    }
}