using System.Windows;
using System.Windows.Controls;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class Login : Window
    {
        private readonly LoginViewModel _viewModel;

        public Login()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            this.DataContext = _viewModel;

            cbLoginMode.SelectionChanged += cbLoginMode_SelectionChanged;
            txtPassword.PasswordChanged += txtPasswordBox_PasswordChanged;

            _viewModel.RequestOpenCustomer += customer =>
            {
                var win = new CustomerWindow(customer);
                win.Show();
                this.Close();
            };

            _viewModel.RequestOpenEmployee += employee =>
            {
                var win = new EmployeeWindow(employee);
                win.Show();
                this.Close();
            };

            _viewModel.RequestLoginFailed += () =>
            {
                MessageBox.Show("Invalid login credentials.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        private void cbLoginMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedMode = (cbLoginMode.SelectedItem as ComboBoxItem)?.Content?.ToString();
            _viewModel.SelectedRole = selectedMode;
        }

        private void txtPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = txtPassword.Password;
        }
    }
}