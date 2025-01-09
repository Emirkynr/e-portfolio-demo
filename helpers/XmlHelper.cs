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
                    var usernameNode = userNode.SelectSingleNode("kisiselBilgiler/Username");
                    var passwordNode = userNode.SelectSingleNode("kisiselBilgiler/Password");

                    if (usernameNode != null && passwordNode != null &&
                        usernameNode.InnerText == username &&
                        passwordNode.InnerText == password)
                    {
                        return new User
                        {
                            Id = int.Parse(userNode.Attributes["ID"].Value),
                            Name = userNode.SelectSingleNode("kisiselBilgiler/FirstName")?.InnerText,
                            Surname = userNode.SelectSingleNode("kisiselBilgiler/LastName")?.InnerText,
                            Email = userNode.SelectSingleNode("kisiselBilgiler/Email")?.InnerText,
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
                    Name = userNode.SelectSingleNode("kisiselBilgiler/FirstName")?.InnerText,
                    Surname = userNode.SelectSingleNode("kisiselBilgiler/LastName")?.InnerText,
                    Email = userNode.SelectSingleNode("kisiselBilgiler/Email")?.InnerText,
                    Username = userNode.SelectSingleNode("kisiselBilgiler/Username")?.InnerText,
                    Password = userNode.SelectSingleNode("kisiselBilgiler/Password")?.InnerText
                };
            }

            return null;
        }

        public void UpdateUser(User user)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");

            var userNode = xmlDoc.SelectSingleNode($"/Users/User[@ID='{user.Id}']");

            if (userNode != null)
            {
                userNode.SelectSingleNode("kisiselBilgiler/FirstName")!.InnerText = user.Name;
                userNode.SelectSingleNode("kisiselBilgiler/LastName")!.InnerText = user.Surname;
                userNode.SelectSingleNode("kisiselBilgiler/GitHub")!.InnerText = user.GitHub;
                userNode.SelectSingleNode("kisiselBilgiler/LinkedIn")!.InnerText = user.LinkedIn;
                userNode.SelectSingleNode("kisiselBilgiler/Email")!.InnerText = user.Email;

                xmlDoc.Save("users.xml");
            }
        }

        public void CreateUser(User user)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");

            var root = xmlDoc.DocumentElement;

            var newUser = xmlDoc.CreateElement("User");
            newUser.SetAttribute("ID", user.Id.ToString());

            var kisiselBilgiler = xmlDoc.CreateElement("kisiselBilgiler");
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "FirstName", user.Name));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "LastName", user.Surname));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "Email", ""));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "Username", user.Username));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "Password", user.Password));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "GitHub", user.GitHub));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "LinkedIn", user.LinkedIn));


            var egitim = xmlDoc.CreateElement("egitim");
            egitim.AppendChild(CreateElement(xmlDoc, "School", ""));
            egitim.AppendChild(CreateElement(xmlDoc, "Degree", ""));
            egitim.AppendChild(CreateElement(xmlDoc, "Field", ""));

            var isDeneyimleri = xmlDoc.CreateElement("isDeneyimleri");
            isDeneyimleri.AppendChild(CreateElement(xmlDoc, "Company", ""));
            isDeneyimleri.AppendChild(CreateElement(xmlDoc, "Position", ""));
            isDeneyimleri.AppendChild(CreateElement(xmlDoc, "Duration", ""));

            var projeler = xmlDoc.CreateElement("projeler");
            projeler.AppendChild(CreateElement(xmlDoc, "Project1", ""));
            projeler.AppendChild(CreateElement(xmlDoc, "Project2", ""));

            var yetenekler = xmlDoc.CreateElement("yetenekler");
            yetenekler.AppendChild(CreateElement(xmlDoc, "Skill1", ""));
            yetenekler.AppendChild(CreateElement(xmlDoc, "Skill2", ""));

            newUser.AppendChild(kisiselBilgiler);
            newUser.AppendChild(egitim);
            newUser.AppendChild(isDeneyimleri);
            newUser.AppendChild(projeler);
            newUser.AppendChild(yetenekler);

            root?.AppendChild(newUser);
            xmlDoc.Save("users.xml");
        }

        private XmlElement CreateElement(XmlDocument doc, string name, string innerText)
        {
            var element = doc.CreateElement(name);
            element.InnerText = innerText;
            return element;
        }
    }
}