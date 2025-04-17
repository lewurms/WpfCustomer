using System.Windows;
using WpfApp2Test2.Models;

namespace WpfApp2Test2
{
    /// <summary>
    /// Interaktionslogik für AddEditCustomer.xaml
    /// </summary>
    public partial class AddEditCustomer : Window
    {
        public AddEditCustomer()
        {
            InitializeComponent();
        }

        private AddEditViewModel viewModel = null;
        public AddEditCustomer(Customer customer)
        {
            viewModel = new AddEditViewModel(customer);
            viewModel.closeAction += OnCloseWindow;
            this.DataContext = viewModel;
            InitializeComponent();

        }

        private void OnCloseWindow(Customer customer)
        {
            viewModel.closeAction -= OnCloseWindow;
            this.Close();
        }

    }
}
