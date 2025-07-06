using System.Windows;
using BusinessObjects;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class CustomerWindow : Window
    {
        private readonly CustomerViewModel _viewModel;

        public CustomerWindow(Customer customer)
        {
            InitializeComponent();

            _viewModel = new CustomerViewModel(customer);
            _viewModel.RequestOpenOrders += customerId =>
            {
                var ordersWindow = new OrderHistoryWindow(customerId)
                {
                    Owner = this
                };
                ordersWindow.ShowDialog();
            };
            _viewModel.RequestLogout += () =>
            {
                var login = new Login();
                login.Show();
                this.Close();
            };
            _viewModel.RequestEditProfile += cus =>
            {
                var editWindow = new EditCustomerWindow(cus)
                {
                    Owner = this
                };

                if (editWindow.ShowDialog() == true)
                {
                    _viewModel.UpdateProfile(editWindow.Customer);
                    _viewModel.RefreshGreeting();
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            };

            DataContext = _viewModel;
        }
    }
}