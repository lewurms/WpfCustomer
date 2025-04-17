using System.Collections.ObjectModel;
using WpfApp2Test2.LoaderService;
using WpfApp2Test2.Models;

namespace WpfApp2Test2.Repository
{
    public class CustomersRepository
    {
        private ObservableCollection<Customer> Customers { set; get; } = new();

        private static CustomersRepository? _instance = null;

        private JsonLoader jsonLoader;
        private XmlLoader xmlLoader;

        public static CustomersRepository Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new();

                return _instance;
            }
        }

        private CustomersRepository()
        {
            //lade hier dinge :D

            this.jsonLoader = new();
            this.xmlLoader = new();

            this.LoadDatas(LoadType.XML);
        }

        ~CustomersRepository()
        {
            //Dispose... lösche alles um speicher freizubekommen
        }

        public void LoadDatas(LoadType type)
        {
            if (type == LoadType.Json)
            {
                #region Json

                var loadedDatas = this.jsonLoader.LoadData(); //hier daten der Liste "zuweisen"!

                if (loadedDatas.Item1 && loadedDatas.customers != null && loadedDatas.customers.Count >= 1)
                {
                    // = new ObservableCollection<DEIN_TYPE>(DEINE_PARAMETER);
                    this.Customers = new ObservableCollection<Customer>(loadedDatas.customers);
                }

                #endregion
            }
            else if (type == LoadType.XML)
            {
                #region Xml
                var loadedDatas = this.xmlLoader.LoadData();

                if (loadedDatas.Item1 && loadedDatas.customers != null && loadedDatas.customers.Count >= 1)
                {
                    this.Customers = new ObservableCollection<Customer>(loadedDatas.customers);
                }

                #endregion
            }
            else if (type == LoadType.Sql)
            {
                #region SQL



                #endregion
            }
        }

        public void SaveDatas()
        {
            this.jsonLoader.SaveData(this.Customers);
            this.xmlLoader.SaveData(this.Customers);
        }

        public void Add(int id, string name, string mail)
        {
            Customer customers = new Customer();
            customers.Id = id;
            customers.Name = name;
            customers.Email = mail;

            this.Add(customers);
        }

        public void Add(Customer customer)
        {
            if (!this.Customers.Contains(customer))
            {
                this.Customers.Add(customer);
            }
        }

        public void Delete(Customer customer)
        {
            if (this.Customers.Contains(customer))
            {
                this.Customers.Remove(customer);
            }
        }

        public void Edit(int id, string email, string name)
        {
            Customer temp = null;
            foreach (Customer customer in this.Customers)
            {
                if (customer.Id.Equals(id))
                {
                    temp = customer;
                    break;
                }
            }

            if (temp is not null)
            {
                var index = this.Customers.IndexOf(temp);
                this.Customers.RemoveAt(index);

                temp.Email = email;
                temp.Name = name;

                this.Customers.Insert(index, temp);
            }
        }

        public ObservableCollection<Customer> GetCustomer()
        {
            return this.Customers;
        }

    }
}
