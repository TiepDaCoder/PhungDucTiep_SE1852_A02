using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using PhungDucTiepWPF.Views;
using Services.Implementation;

namespace PhungDucTiepWPF.ViewModels
{
    public class CustomerManagementViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService;
        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer { get; set; }
        public string SearchKeyword { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        public CustomerManagementViewModel()
        {
            _customerService = new CustomerService();
            Customers = new ObservableCollection<Customer>(_customerService.GetAll());

            AddCommand = new RelayCommand(AddCustomer);
            EditCommand = new RelayCommand(EditCustomer, () => SelectedCustomer != null);
            DeleteCommand = new RelayCommand(DeleteCustomer, () => SelectedCustomer != null);
            SearchCommand = new RelayCommand(SearchCustomers);
        }

        private void AddCustomer()
        {
            var customer = new Customer();
            var dialog = new CustomerDialog(customer);
            if (dialog.ShowDialog() == true)
            {
                _customerService.Add(customer);
                Customers.Add(customer);
            }
        }

        private void EditCustomer()
        {
            var cloned = new Customer
            {
                CustomerID = SelectedCustomer.CustomerID,
                CompanyName = SelectedCustomer.CompanyName,
                ContactName = SelectedCustomer.ContactName,
                ContactTitle = SelectedCustomer.ContactTitle,
                Address = SelectedCustomer.Address,
                Phone = SelectedCustomer.Phone
            };

            var dialog = new CustomerDialog(cloned);
            if (dialog.ShowDialog() == true)
            {
                _customerService.UpdateCustomer(cloned);
                var index = Customers.IndexOf(SelectedCustomer);
                Customers[index] = cloned;
            }
        }

        private void DeleteCustomer()
        {
            var result = MessageBox.Show($"Delete Customer {SelectedCustomer.ContactName}?", "Confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _customerService.Delete(SelectedCustomer.CustomerID);
                Customers.Remove(SelectedCustomer);
            }
        }

        private void SearchCustomers()
        {
            var results = _customerService.Search(SearchKeyword ?? "");
            Customers.Clear();
            foreach (var c in results) Customers.Add(c);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}