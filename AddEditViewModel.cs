using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using WpfApp2Test2.LoaderService;

namespace WpfApp2Test2
{
    public class AddEditViewModel : INotifyPropertyChanged
    {
        #region Attribute

        public Action<Customer> closeAction;
        public bool CanCancel => true;
        public event PropertyChangedEventHandler PropertyChanged;

        private Customer customer;
        private ICommand _cancelCommand;



        #endregion
        public AddEditViewModel(Customer customer)
        {
            if (customer is null)
                return;

            this.customer = customer;
        }


        #region Methoden

        private void CloseWindow()
        {
            closeAction?.Invoke(this.customer);
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
