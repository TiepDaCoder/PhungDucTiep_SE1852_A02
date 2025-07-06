using System.Windows;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class OrderManagementWindow : Window
    {
        public OrderManagementWindow()
        {
            InitializeComponent();
            DataContext = new OrderManagementViewModel();
        }
    }
}