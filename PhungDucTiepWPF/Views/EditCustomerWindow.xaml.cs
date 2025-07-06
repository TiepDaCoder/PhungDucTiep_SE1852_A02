using System.Windows;
using BusinessObjects;

namespace PhungDucTiepWPF.Views
{
    public partial class EditCustomerWindow : Window
    {
        public Customer Customer { get; private set; }

        public EditCustomerWindow(Customer customer)
        {
            InitializeComponent();
            // Tạo bản sao để không thay đổi trực tiếp khi chưa lưu
            Customer = new Customer
            {
                CustomerID = customer.CustomerID,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                Phone = customer.Phone
            };
            this.DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}