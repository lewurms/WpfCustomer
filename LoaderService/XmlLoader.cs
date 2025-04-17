using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using WpfApp2Test2.Models;
using static System.Environment;

namespace WpfApp2Test2.LoaderService
{
    public class XmlLoader : LoaderInterface
    {
        public string GetPath()
        {
            return $"{Environment.GetFolderPath(SpecialFolder.Desktop)}\\CustomerData.xml";
        }

        public (bool, IList<Customer> customers) LoadData()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                XDocument doc = XDocument.Load(GetPath());
                XElement root = doc.Root;

                if (root != null)
                {
                    foreach (XElement customerElement in root.Elements("Customer"))
                    {
                        int id = 0;
                        string name = "";
                        string email = "";

                        XElement idElement = customerElement.Element("Id");
                        if (idElement != null && int.TryParse(idElement.Value, out int parsedId))
                            id = parsedId;

                        XElement nameElement = customerElement.Element(TableConstants.NAME);
                        if (nameElement != null)
                            name = nameElement.Value;

                        XElement emailElement = customerElement.Element("Mail");
                        if (emailElement != null)
                            email = emailElement.Value;

                        Customer customer = new Customer
                        {
                            Id = id,
                            Name = name,
                            Email = email
                        };

                        customers.Add(customer);
                    }
                }

                return (true, customers);
            }
            catch (Exception ex)
            {
                return (false, new List<Customer>());
            }
        }

        public (bool, string) SaveData(IList<Customer> customers)
        {
            try
            {
                string xmlString = this.GetXml(customers);
                string path = this.GetPath();
                File.WriteAllText(path, xmlString);

                return (true, xmlString);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        private string? GetXml(IList<Customer> customers)
        {
            try
            {
                string? xmlString = null;


                List<XElement> xmlCustomers = new();

                foreach (Customer customer in customers)
                {
                    List<XElement> customerAttributes = new();

                    XElement xmlElementId = new XElement("Id", customer.Id);
                    customerAttributes.Add(xmlElementId);

                    XElement xmlElementName = new XElement(TableConstants.NAME, customer.Name);
                    customerAttributes.Add(xmlElementName);

                    XElement xmlElementMail = new XElement("Mail", customer.Email);
                    customerAttributes.Add(xmlElementMail);

                    XElement xElementParent = new("Customer", customerAttributes);
                    xmlCustomers.Add(xElementParent);
                }

                XElement xElementRoot = new("Customers", xmlCustomers);
                XDocument xmlDocument = new XDocument(xElementRoot);

                return xmlDocument.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
