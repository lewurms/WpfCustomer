using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp2Test2.Models;

namespace WpfApp2Test2
{
    public class AddEditViewModel : INotifyPropertyChanged
    {
        #region Attribute

        private string _name;
        public string Name
        {
            set
            {
                this._name = value;
                this.OnPropertyChanged(nameof(Name));
            }
            get
            {
                return this._name;
            }
        }

        private string _email;
        public string Email
        {
            set
            {
                this._email = value;
                this.OnPropertyChanged(nameof(Email));
            }
            get
            {
                return this._email;
            }
        }

        public Action<Customer> closeAction;
        public bool CanCancel => true;
        public event PropertyChangedEventHandler PropertyChanged;

        public Customer Customer { set; get; }
        private ICommand _cancelCommand;

        #endregion

        public AddEditViewModel(Customer customer)
        {
            if (customer is null)
                return;

            this.Customer = customer;
            this.Email = customer.Email;
            this.Name = customer.Name;
        }

        #region Methoden

        private void CloseWindow()
        {
            closeAction?.Invoke(this.Customer);
        }



        #endregion

        #region Events && Commands

        /// <summary>
        /// Teilt der UI änderungen mit
        /// </summary>
        /// <param name="propertyName">NAme der Variable dessen Wert sich geändert hat, wo die Änderungn an die Ui mitgeteilt werden soll</param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(
                    () => CloseWindow(),
                    () => CanCancel
                    ));
            }
        }



        #endregion
    }
}
