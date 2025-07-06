using System.ComponentModel;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using Services.Implementation;

namespace PhungDucTiepWPF.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        private Employee _employee;

        public string Greeting { get; set; }

        public ICommand ManageCustomersCommand { get; }
        public ICommand ManageProductsCommand { get; }
        public ICommand ManageOrdersCommand { get; }
        public ICommand ReportsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand EditProfileCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action? RequestOpenCustomers;
        public event Action? RequestOpenProducts;
        public event Action? RequestOpenOrders;
        public event Action? RequestOpenReports;
        public event Action? RequestLogout;
        public event Action<Employee>? RequestEditProfile;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EmployeeViewModel(Employee employee)
        {
            _employee = employee;
            _employeeService = new EmployeeService();
            Greeting = $"Welcome, {employee.Name}!";

            ManageCustomersCommand = new RelayCommand(OnManageCustomers);
            ManageProductsCommand = new RelayCommand(OnManageProducts);
            ManageOrdersCommand = new RelayCommand(OnManageOrders);
            ReportsCommand = new RelayCommand(OnReports);
            LogoutCommand = new RelayCommand(OnLogout);
            EditProfileCommand = new RelayCommand(OnEditProfile);
        }

        private void OnManageCustomers() => RequestOpenCustomers?.Invoke();
        private void OnManageProducts() => RequestOpenProducts?.Invoke();
        private void OnManageOrders() => RequestOpenOrders?.Invoke();
        private void OnReports() => RequestOpenReports?.Invoke();
        private void OnLogout() => RequestLogout?.Invoke();
        private void OnEditProfile() => RequestEditProfile?.Invoke(_employee);

        public void UpdateProfile(Employee updated)
        {
            _employee.Name = updated.Name;
            _employee.UserName = updated.UserName;
            _employee.Password = updated.Password;
            _employee.JobTitle = updated.JobTitle;
            _employee.BirthDate = updated.BirthDate;
            _employee.HireDate = updated.HireDate;
            _employee.Address = updated.Address;

            _employeeService.UpdateProfile(_employee);
            RefreshGreeting();
        }

        public void RefreshGreeting()
        {
            Greeting = $"Welcome, {_employee.Name}!";
            OnPropertyChanged(nameof(Greeting));
        }
    }
}