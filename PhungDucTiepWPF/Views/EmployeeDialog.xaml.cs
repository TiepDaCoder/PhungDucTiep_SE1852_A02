using System.Windows;
using BusinessObjects;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class EmployeeDialog : Window
    {
        public Employee UpdatedEmployee { get; private set; }

        public EmployeeDialog(Employee employee)
        {
            InitializeComponent();
            UpdatedEmployee = employee;

            var vm = new EmployeeDialogViewModel(employee);
            vm.RequestCloseWithSuccess += () =>
            {
                DialogResult = true;
                Close();
            };
            vm.RequestClose += () => Close();

            DataContext = vm;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}