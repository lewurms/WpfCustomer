using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
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
            throw new NotImplementedException();
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
