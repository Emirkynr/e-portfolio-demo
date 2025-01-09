// Helpers/XmlHelper.cs
using System.Xml.Linq;
using e_portfolio.Models;
using System.Linq;
using System.Xml;


namespace e_portfolio.Helpers
{
    public class XmlHelper
    {
        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Surname { get; set; }
            public string? Email { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        public User? GetUserByUsernameAndPassword(string username, string password)
        {
            // XML dosyasını yükleyin
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");

            // Tüm kullanıcı düğümlerini alın
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
                        // Kullanıcı bulundu, User nesnesini oluşturup döndürün
                        return new User
                        {
                            Id = int.Parse(userNode.Attributes?["Id"]?.Value ?? "0"),
                            Name = userNode.SelectSingleNode("Name")?.InnerText,
                            Surname = userNode.SelectSingleNode("Surname")?.InnerText,
                            Email = userNode.SelectSingleNode("Email")?.InnerText,
                            Username = usernameNode.InnerText,
                            Password = passwordNode.InnerText
                        };
                    }
                }
            }

            // Kullanıcı bulunamadıysa null döndür
            return null;
        }

        public User? GetUserById(int id)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");

            var userNode = xmlDoc.SelectSingleNode($"/Users/User[@Id='{id}']");

            if (userNode != null)
            {
                return new User
                {
                    Id = id,
                    Name = userNode.SelectSingleNode("Name")?.InnerText,
                    Surname = userNode.SelectSingleNode("Surname")?.InnerText,
                    Email = userNode.SelectSingleNode("Email")?.InnerText,
                    Username = userNode.SelectSingleNode("Username")?.InnerText,
                    Password = userNode.SelectSingleNode("Password")?.InnerText
                };
            }

            return null;
        }


    }
}
