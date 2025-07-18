using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using Services.Implementation;

namespace PhungDucTiepWPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _phone;
        private string _password;
        private string _userName;
        private string _selectedRole;

        private readonly CustomerService _customerService = new CustomerService();
        private readonly EmployeeService _employeeService = new EmployeeService();

        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
        }

        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCustomer));
                OnPropertyChanged(nameof(IsEmployee));
            }
        }

        public bool IsCustomer => SelectedRole == "Customer Login";
        public bool IsEmployee => SelectedRole == "Employee Login";

        public ICommand LoginCommand { get; }

        public Customer LoggedInCustomer { get; private set; }
        public Employee LoggedInEmployee { get; private set; }

        public LoginViewModel()
        {
            SelectedRole = "Employee Login";

            //Login nhanh cho nhân viên
            //UserName = "Nancy";
            //Password = "123";

            //Login nhanh cho khách hàng
            //Phone = "030-0074321";
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private void ExecuteLogin()
        {
            if (IsCustomer)
            {
                var customer = _customerService.Login(Phone);
                if (customer != null)
                {
                    LoggedInCustomer = customer;
                    RequestOpenCustomer?.Invoke(customer);
                    return;
                }
            }
            else if (IsEmployee)
            {
                var employee = _employeeService.Login(UserName, Password);
                if (employee != null)
                {
                    LoggedInEmployee = employee;
                    RequestOpenEmployee?.Invoke(employee);
                    return;
                }
            }

            RequestLoginFailed?.Invoke();
        }

        public event Action<Customer> RequestOpenCustomer;
        public event Action<Employee> RequestOpenEmployee;
        public event Action RequestLoginFailed;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}