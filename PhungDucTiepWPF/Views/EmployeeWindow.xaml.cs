using System.Windows;
using BusinessObjects;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class EmployeeWindow : Window
    {
        private readonly EmployeeViewModel _viewModel;

        public EmployeeWindow(Employee employee)
        {
            InitializeComponent();

            _viewModel = new EmployeeViewModel(employee);

            _viewModel.RequestOpenCustomers += OpenCustomerManagement;
            _viewModel.RequestOpenProducts += OpenProductManagement;
            _viewModel.RequestOpenOrders += OpenOrderManagement;
            _viewModel.RequestOpenReports += OpenReportWindow;
            _viewModel.RequestLogout += Logout;
            _viewModel.RequestEditProfile += OpenEditProfileDialog;

            DataContext = _viewModel;
        }

        private void OpenCustomerManagement()
        {
            var window = new CustomerManagementWindow
            {
                Owner = this
            };
            window.ShowDialog();
        }

        private void OpenProductManagement()
        {
            var window = new ProductManagementWindow
            {
                Owner = this
            };
            window.ShowDialog();
        }

        private void OpenOrderManagement()
        {
            var window = new OrderManagementWindow
            {
                Owner = this
            };
            window.ShowDialog();
        }

        private void OpenReportWindow()
        {
            var window = new ReportWindow
            {
                Owner = this
            };
            window.ShowDialog();
        }

        private void Logout()
        {
            var loginWindow = new Login();
            loginWindow.Show();
            Close();
        }

        private void OpenEditProfileDialog(Employee emp)
        {
            var dialog = new EmployeeDialog(emp)
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                _viewModel.UpdateProfile(dialog.UpdatedEmployee);
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}