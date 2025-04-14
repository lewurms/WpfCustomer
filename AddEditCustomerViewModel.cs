using System.Collections.ObjectModel;

namespace WpfApp2Test2
{
    internal class AddEditCustomerViewModel
    {
        private ObservableCollection<Customer> customers;
        private Customer selectedCustomer;

        public AddEditCustomerViewModel(Customer selectedCustomer)
        {
            this.selectedCustomer = selectedCustomer;
        }

        public AddEditCustomerViewModel(ObservableCollection<Customer> customers, Customer selectedCustomer)
        {
            this.customers = customers;
            this.selectedCustomer = selectedCustomer;
        }
    }
}