using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using WpfApp2Test2.LoaderService;

namespace WpfApp2Test2
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public CustomerViewModel()
        {
            this.jsonLoader = new();
            this.xmlLoader = new();
            var loadedDatas = this.jsonLoader.LoadData(); //hier daten der Liste "zuweisen"!
            if (loadedDatas.Item1 && loadedDatas.customers != null && loadedDatas.customers.Count >= 1)
            {
                            // = new ObservableCollection<DEIN_TYPE>(DEINE_PARAMETER);
                this.Customers = new ObservableCollection<Customer>(loadedDatas.customers);
            }
        }

        #region Attribute

        /// <summary>
        /// Wert der angibt wie viele Test Daten erstellt werden sollen.
        /// </summary>
        private const int TEST_DATAS_COUNT = 10;

        public event PropertyChangedEventHandler PropertyChanged;

        //[TS] Vorsicht beim editieren kann es sein, dass die liste zwischengespeichert werden , geleert und wieder hinzugefügt werden muss.
        public ObservableCollection<Customer> Customers { set; get; } = new();

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            set
            {
                this._selectedCustomer = value;
                this.OnPropertyChanged(nameof(SelectedCustomer));
            }
            get
            {
                return this._selectedCustomer;
            }
        }

        private string _beispiel;

        public string Beispiel
        {
            set
            {
                this._beispiel = value;
                this.OnPropertyChanged(nameof(Beispiel));
            }
            get
            {
                return this._beispiel;
            }
        }

        private string _mail;

        public string Mail
        {
            set
            {
                this._mail = value;
                this.OnPropertyChanged(nameof(Mail));
            }
            get
            {
                return this._mail;
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            set
            {
                this._errorMessage = value;
                this.OnPropertyChanged(nameof(ErrorMessage));
            }
            get
            {
                return this._errorMessage; ;
            }
        }

        public bool CanDeleteCustomer
        {
            get
            {
                return SelectedCustomer != null;
            }
        }

        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public ICommand _editCustomerCommand;

        private ICommand _addCustomerCommand;

        // Löscht den ausgewählten Eintrag
        private ICommand _deleteCustomerCommand;

        private JsonLoader jsonLoader;
        private XmlLoader xmlLoader;

        #endregion

        #region Methoden

        public void SaveDatas()
        {
            this.jsonLoader.SaveData(this.Customers);
            this.xmlLoader.SaveData(this.Customers);
        }

        public void EditCustomer()
        {
            AddEditCustomer addEditCustomer = new AddEditCustomer(this.SelectedCustomer);
            addEditCustomer.ShowDialog();

            this.SaveDatas();
        }

        public void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;

                this.SaveDatas();
            }
        }

        public void AddCustomer()
        {
            Customer customers = new Customer();
            customers.Id = Customers.Count + 1;
            customers.Name = this.Beispiel;//"Lisa";
            customers.Email = this.Mail;//"lisa@outlook.com"

            if (!CheckUserInput(customers))
            {
                ShowToast("Bitte überprüfen Sie ihre Eingabe.");
                return; // Falls die Eingabe ungültig ist, abbrechen
            }

            // Überprüfung auf Duplikate 
            if (IsDuplicated(customers.Name, Customers) || IsDuplicated(customers.Email, Customers))
            {
                ShowToast("Ein Kunde mit diesem Namen oder dieser E-Mail existiert bereits.");
                return;
            }


            Customers.Add(customers);
            ShowToast("Kunde erfolgreich hinzugefügt.");
            this.SaveDatas();
        }

        //Prüfe Name und Email auf Korrektheit der Eingabe

        //1ster durchlauf...customer.email =>"", customer.Name =>"Test"
        //2ter durchlauf ...customer.email =>"test", customer.Name =>"Test"

        // Prüft Duplikate
        private bool IsDuplicated(string valueToCheck, IList<Customer> list)
        {
            if (list.Count == 0)
                return false;


            foreach (Customer customer in list)
            {
                if (customer.Name.Equals(valueToCheck) || customer.Email.Equals(valueToCheck))
                    return true;
            }

            return false;
        }

        private bool CheckUserInput(Customer customer)
        {
            bool isNullOrEmpty = string.IsNullOrEmpty(customer.Email);
            bool contains = !string.IsNullOrEmpty(customer.Email) && !customer.Email.Contains("@");


            if (isNullOrEmpty || contains)//Boolesche logische Operatoren 
            {
                return false;
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// Zeigt dem Nutzer den <paramref name="messageToShow"/> an
        /// </summary>
        /// <param name="messageToShow"></param>
        private void ShowToast(string messageToShow)
        {
            this.ErrorMessage = messageToShow;

            Timer timer = new(1000);

            timer.Elapsed += (o, e) =>
            {
                this.ErrorMessage = string.Empty;
                timer.Stop();
            };


            timer.Start();

        }

        #endregion

        #region Commands 

        public ICommand EditCustomerCommand
        {
            get
            {
                return _editCustomerCommand ?? (_editCustomerCommand = new CommandHandler(
                    () => EditCustomer(),
                    () => CanDeleteCustomer
                    ));
            }
        }

        public ICommand AddCustomerCommand
        {
            get
            {
                return _addCustomerCommand ?? (_addCustomerCommand = new CommandHandler(
                    () => AddCustomer(),
                    () => CanExecute
                    ));
            }
        }

        public ICommand DeleteCustomerCommand
        {

            get
            {
                return _deleteCustomerCommand ?? (_deleteCustomerCommand = new CommandHandler(
                  () => DeleteCustomer(),
                  () => CanDeleteCustomer
                  ));
            }
        }

        /// <summary>
        /// Teilt der UI änderungen mit
        /// </summary>
        /// <param name="propertyName">NAme der Variable dessen Wert sich geändert hat, wo die Änderungn an die Ui mitgeteilt werden soll</param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

    }
}


