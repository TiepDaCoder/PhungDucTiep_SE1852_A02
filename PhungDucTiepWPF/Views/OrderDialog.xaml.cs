using System.Windows;
using BusinessObjects;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class OrderDialog : Window
    {
        public OrderDialog(Order? order = null)
        {
            InitializeComponent();

            var vm = order == null ? new OrderDialogViewModel() : new OrderDialogViewModel(order);

            vm.RequestCloseWithSuccess += () => { DialogResult = true; Close(); };
            vm.RequestClose += () => { DialogResult = false; Close(); };

            DataContext = vm;
        }
    }
}
