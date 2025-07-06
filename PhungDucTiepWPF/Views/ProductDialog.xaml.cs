using System.Windows;
using BusinessObjects;
using PhungDucTiepWPF.ViewModels;

namespace PhungDucTiepWPF.Views
{
    public partial class ProductDialog : Window
    {
        public Product Product { get; set; }

        public ProductDialog(Product product)
        {
            InitializeComponent();
            var vm = new ProductDialogViewModel(product);
            vm.RequestCloseWithSuccess += () =>
            {
                DialogResult = true;
                Close();
            };
            vm.RequestClose += () => Close();
            DataContext = vm;
        }
    }
}