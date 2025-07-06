using System.ComponentModel;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using Services.Implementation;

namespace PhungDucTiepWPF.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService;
        public string Greeting { get; set; }
        public Customer _customer { get; set; }

        public ICommand EditProfileCommand { get; }
        public ICommand ViewOrdersCommand { get; }
        public ICommand LogoutCommand { get; }

        public event Action RequestLogout;
        public event Action<Customer> RequestEditProfile;
        public event Action<int> RequestOpenOrders;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
            Greeting = $"Welcome, {customer.ContactName}!";
            _customerService = new CustomerService();

            EditProfileCommand = new RelayCommand(OnEditProfile);
            ViewOrdersCommand = new RelayCommand(OnViewOrders);
            LogoutCommand = new RelayCommand(OnLogout);
        }

        private void OnEditProfile()
        {
            RequestEditProfile?.Invoke(_customer);
        }

        private void OnViewOrders()
        {
            RequestOpenOrders?.Invoke(_customer.CustomerID);
        }

        private void OnLogout()
        {
            RequestLogout?.Invoke();
        }

        public void UpdateProfile(Customer updated)
        {
            _customer.CompanyName = updated.CompanyName;
            _customer.ContactName = updated.ContactName;
            _customer.ContactTitle = updated.ContactTitle;
            _customer.Address = updated.Address;
            _customer.Phone = updated.Phone;

            _customerService.UpdateCustomer(_customer);
            RefreshGreeting();
        }

        public void RefreshGreeting()
        {
            Greeting = $"Welcome, {_customer.ContactName}!";
            OnPropertyChanged(nameof(Greeting));
        }

    }
}