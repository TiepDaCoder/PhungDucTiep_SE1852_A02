using System.Windows;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class ProductManagementWindow : Window
    {
        public ProductManagementWindow()
        {
            InitializeComponent();
            DataContext = new ProductManagementViewModel();
        }
    }
}