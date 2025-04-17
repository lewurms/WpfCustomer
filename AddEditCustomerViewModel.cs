using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp2Test2.Models;
using WpfApp2Test2.Repository;

namespace WpfApp2Test2
{
    internal class AddEditCustomerViewModel : INotifyPropertyChanged
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

        public Action closeAction;
        public bool CanCancel => true;
        public bool CanSave => true;
        public event PropertyChangedEventHandler PropertyChanged;

        public Customer Customer { set; get; }
        private ICommand _cancelCommand;
        private ICommand _saveCommand;

        #endregion

        public AddEditCustomerViewModel(Customer customer)
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
            closeAction?.Invoke();
        }

        private void SaveEdit()
        {
            CustomersRepository.Instance.Edit(this.Customer.Id, this.Email, this.Name);

            closeAction?.Invoke();
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

        public ICommand SaveCustomerCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(
                    () => SaveEdit(),
                    () => CanSave
                    ));
            }
        }



        #endregion
    }
}