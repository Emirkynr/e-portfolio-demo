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
                            Password = passwordNode.InnerText,
                            GitHub = userNode.SelectSingleNode("kisiselBilgiler/GitHub")?.InnerText,
                            LinkedIn = userNode.SelectSingleNode("kisiselBilgiler/LinkedIn")?.InnerText,
                            TelNo = userNode.SelectSingleNode("kisiselBilgiler/TelNo")?.InnerText,
                            okulAdi = userNode.SelectSingleNode("egitim/okulAdi")?.InnerText,
                            bolumAdi = userNode.SelectSingleNode("egitim/bolumAdi")?.InnerText,
                            egitimDüzeyi = userNode.SelectSingleNode("egitim/egitimDüzeyi")?.InnerText,
                            egitimYillari = userNode.SelectSingleNode("egitim/egitimYillari")?.InnerText
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
                    GitHub = userNode.SelectSingleNode("kisiselBilgiler/GitHub")?.InnerText,
                    LinkedIn = userNode.SelectSingleNode("kisiselBilgiler/LinkedIn")?.InnerText,
                    TelNo = userNode.SelectSingleNode("kisiselBilgiler/TelNo")?.InnerText,
                    Password = userNode.SelectSingleNode("kisiselBilgiler/Password")?.InnerText,
                    okulAdi = userNode.SelectSingleNode("egitim/okulAdi")?.InnerText,
                    bolumAdi = userNode.SelectSingleNode("egitim/bolumAdi")?.InnerText,
                    egitimDüzeyi = userNode.SelectSingleNode("egitim/egitimDüzeyi")?.InnerText,
                    egitimYillari = userNode.SelectSingleNode("egitim/egitimYillari")?.InnerText
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
                UpdateNode(userNode.SelectSingleNode("kisiselBilgiler/FirstName"), user.Name);
                UpdateNode(userNode.SelectSingleNode("kisiselBilgiler/LastName"), user.Surname);
                UpdateNode(userNode.SelectSingleNode("kisiselBilgiler/GitHub"), user.GitHub);
                UpdateNode(userNode.SelectSingleNode("kisiselBilgiler/LinkedIn"), user.LinkedIn);
                UpdateNode(userNode.SelectSingleNode("kisiselBilgiler/TelNo"), user.TelNo);
                UpdateNode(userNode.SelectSingleNode("kisiselBilgiler/Password"), user.Password);
                UpdateNode(userNode.SelectSingleNode("kisiselBilgiler/Email"), user.Email);
                UpdateNode(userNode.SelectSingleNode("egitim/okulAdi"), user.okulAdi);
                UpdateNode(userNode.SelectSingleNode("egitim/bolumAdi"), user.bolumAdi);
                UpdateNode(userNode.SelectSingleNode("egitim/egitimDüzeyi"), user.egitimDüzeyi);
                UpdateNode(userNode.SelectSingleNode("egitim/egitimYillari"), user.egitimYillari);

                xmlDoc.Save("users.xml");
            }
        }

        private void UpdateNode(XmlNode? node, string? value)
        {
            if (node != null && value != null)
            {
                node.InnerText = value;
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
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "Email", user.Email));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "Username", user.Username));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "Password", user.Password));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "TelNo", user.TelNo));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "GitHub", user.GitHub));
            kisiselBilgiler.AppendChild(CreateElement(xmlDoc, "LinkedIn", user.LinkedIn));

            var egitim = xmlDoc.CreateElement("egitim");
            egitim.AppendChild(CreateElement(xmlDoc, "okulAdi", user.okulAdi));
            egitim.AppendChild(CreateElement(xmlDoc, "bolumAdi", user.bolumAdi));
            egitim.AppendChild(CreateElement(xmlDoc, "egitimDüzeyi", user.egitimDüzeyi));
            egitim.AppendChild(CreateElement(xmlDoc, "egitimYillari", user.egitimYillari));

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

        private XmlElement CreateElement(XmlDocument doc, string name, string? innerText)
        {
            var element = doc.CreateElement(name);
            element.InnerText = innerText ?? string.Empty;
            return element;
        }
    }
}