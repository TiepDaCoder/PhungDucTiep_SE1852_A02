using System.Windows;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerManagementWindow.xaml
    /// </summary>
    public partial class CustomerManagementWindow : Window
    {
        public CustomerManagementWindow()
        {
            InitializeComponent();
            DataContext = new CustomerManagementViewModel();
        }
    }
}