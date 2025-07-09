using System.Windows;
using BusinessObjects;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class CustomerDialog : Window
    {
        public Customer Customer { get; private set; }

        public CustomerDialog(Customer customer)
        {
            InitializeComponent();
            Customer = customer;

            var vm = new CustomerDialogViewModel(customer);
            vm.RequestCloseWithSuccess += () =>
            {
                DialogResult = true;
                Close();
            };
            vm.RequestClose += () => Close();

            DataContext = vm;
        }
    }
}