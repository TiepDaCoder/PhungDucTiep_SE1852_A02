using System.Windows;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class OrderHistoryWindow : Window
    {
        public OrderHistoryWindow(int customerId)
        {
            InitializeComponent();
            DataContext = new OrderHistoryViewModel(customerId);
        }
    }
}