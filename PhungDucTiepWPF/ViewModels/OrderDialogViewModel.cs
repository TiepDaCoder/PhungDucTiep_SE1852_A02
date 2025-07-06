using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using Services.Implementation;

namespace PhungDucTiepWPF.ViewModels
{
    public class OrderDialogViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService = new();
        private readonly EmployeeService _employeeService = new();
        private readonly ProductService _productService = new();
        private readonly OrderService _orderService = new();

        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<OrderDetailViewModel> OrderDetails { get; set; }

        public Customer SelectedCustomer { get; set; }
        public Employee SelectedEmployee { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Today;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action? RequestCloseWithSuccess;
        public event Action? RequestClose;

        public OrderDialogViewModel()
        {
            Customers = new ObservableCollection<Customer>(_customerService.GetAll());
            Employees = new ObservableCollection<Employee>(_employeeService.GetAll());
            Products = new ObservableCollection<Product>(_productService.GetAll());

            OrderDetails = new ObservableCollection<OrderDetailViewModel>();
            OrderDetails.Add(new OrderDetailViewModel(Products));

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }
        public OrderDialogViewModel(Order order) : this()
        {
            SelectedCustomer = Customers.FirstOrDefault(c => c.CustomerID == order.CustomerID);
            SelectedEmployee = Employees.FirstOrDefault(e => e.EmployeeID == order.EmployeeID);
            OrderDate = order.OrderDate;

            var existingDetails = _orderService.GetDetailsByOrderId(order.OrderID);
            foreach (var d in existingDetails)
            {
                var vm = new OrderDetailViewModel(Products)
                {
                    Product = Products.FirstOrDefault(p => p.ProductID == d.ProductID),
                    Quantity = d.Quantity,
                    Discount = d.Discount
                };
                OrderDetails.Add(vm);
            }
        }

        public Order Order => new Order
        {
            CustomerID = SelectedCustomer?.CustomerID ?? 0,
            EmployeeID = SelectedEmployee?.EmployeeID ?? 0,
            OrderDate = OrderDate
        };

        public List<OrderDetail> OrderDetailResults =>
            OrderDetails
                .Where(d => d.Product != null)
                .Select(d => new OrderDetail
                {
                    ProductID = d.Product.ProductID,
                    UnitPrice = d.Product.UnitPrice ?? 0,
                    Quantity = d.Quantity,
                    Discount = d.Discount
                }).ToList();

        private void OnSave()
        {
            if (SelectedCustomer == null || SelectedEmployee == null || OrderDetailResults.Count == 0)
            {
                MessageBox.Show("Please fill all required fields and add at least one product.", "Validation",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RequestCloseWithSuccess?.Invoke();
        }

        private void OnCancel() => RequestClose?.Invoke();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}