using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using PhungDucTiepWPF.Views;
using Services.Implementation;
using Services.Interface;

namespace PhungDucTiepWPF.ViewModels
{
    public class OrderManagementViewModel : INotifyPropertyChanged
    {
        private readonly IOrderService _orderService;

        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<OrderDetail> OrderDetails { get; set; }
        public ICommand EditCommand { get; }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
                LoadOrderDetails();
            }
        }

        public string CustomerIDFilter { get; set; }
        public string EmployeeIDFilter { get; set; }

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public OrderManagementViewModel()
        {
            _orderService = new OrderService();
            Orders = new ObservableCollection<Order>(_orderService.GetAllOrders());
            OrderDetails = new ObservableCollection<OrderDetail>();

            SearchCommand = new RelayCommand(SearchOrders);
            AddCommand = new RelayCommand(AddOrder);
            DeleteCommand = new RelayCommand(DeleteOrder, () => SelectedOrder != null);
            EditCommand = new RelayCommand(EditOrder, () => SelectedOrder != null);
        }

        private void LoadOrderDetails()
        {
            OrderDetails.Clear();
            if (SelectedOrder != null)
            {
                var details = _orderService.GetDetailsByOrderId(SelectedOrder.OrderID);
                foreach (var detail in details)
                    OrderDetails.Add(detail);
            }
        }

        private void SearchOrders()
        {
            int? customerId = int.TryParse(CustomerIDFilter, out int cId) ? cId : null;
            int? employeeId = int.TryParse(EmployeeIDFilter, out int eId) ? eId : null;

            var results = _orderService.SearchOrders(customerId, employeeId);
            Orders.Clear();
            foreach (var o in results)
                Orders.Add(o);
        }

        private void AddOrder()
        {
            var dialog = new OrderDialog();
            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as OrderDialogViewModel;
                var newOrder = vm.Order;

                _orderService.AddOrder(newOrder);

                foreach (var detail in vm.OrderDetailResults)
                {
                    detail.OrderID = newOrder.OrderID;
                    _orderService.AddOrderDetail(detail);
                }

                Orders.Add(newOrder);
                MessageBox.Show("Order added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DeleteOrder()
        {
            if (SelectedOrder == null) return;

            var result = MessageBox.Show($"Delete Order {SelectedOrder.OrderID}?", "Confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _orderService.DeleteOrder(SelectedOrder.OrderID);
                Orders.Remove(SelectedOrder);
                OrderDetails.Clear();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void EditOrder()
        {
            if (SelectedOrder == null) return;

            var dialog = new OrderDialog(SelectedOrder); // constructor mới
            if (dialog.ShowDialog() == true)
            {
                var vm = dialog.DataContext as OrderDialogViewModel;
                var updatedOrder = vm.Order;

                // Cập nhật order
                _orderService.DeleteOrder(SelectedOrder.OrderID); // xoá cũ
                _orderService.AddOrder(updatedOrder);             // thêm mới

                foreach (var detail in vm.OrderDetailResults)
                {
                    detail.OrderID = updatedOrder.OrderID;
                    _orderService.AddOrderDetail(detail);
                }

                Orders[Orders.IndexOf(SelectedOrder)] = updatedOrder;
                SelectedOrder = updatedOrder;

                MessageBox.Show("Order updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}