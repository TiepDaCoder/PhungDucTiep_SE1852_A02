using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using Services.Implementation;
using Services.Interface;

namespace PhungDucTiepWPF.ViewModels
{
    public class ProductManagementViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private Product _selectedProduct;
        private string _searchKeyword;

        public ObservableCollection<Product> Products { get; set; }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public string SearchKeyword
        {
            get => _searchKeyword;
            set { _searchKeyword = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductManagementViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>(_productService.GetAll());

            AddCommand = new RelayCommand(AddProduct);
            EditCommand = new RelayCommand(EditProduct, () => SelectedProduct != null);
            DeleteCommand = new RelayCommand(DeleteProduct, () => SelectedProduct != null);
            SearchCommand = new RelayCommand(SearchProduct);
        }

        private void AddProduct()
        {
            var newProduct = new Product();
            var dialog = new Views.ProductDialog(newProduct);
            if (dialog.ShowDialog() == true)
            {
                _productService.Add(newProduct);
                Products.Add(newProduct);
            }
        }

        private void EditProduct()
        {
            if (SelectedProduct == null) return;

            var clonedProduct = new Product
            {
                ProductID = SelectedProduct.ProductID,
                ProductName = SelectedProduct.ProductName,
                SupplierID = SelectedProduct.SupplierID,
                CategoryID = SelectedProduct.CategoryID,
                QuantityPerUnit = SelectedProduct.QuantityPerUnit,
                UnitPrice = SelectedProduct.UnitPrice,
                UnitsInStock = SelectedProduct.UnitsInStock,
                UnitsOnOrder = SelectedProduct.UnitsOnOrder,
                ReorderLevel = SelectedProduct.ReorderLevel,
                Discontinued = SelectedProduct.Discontinued
            };

            var dialog = new Views.ProductDialog(clonedProduct);
            if (dialog.ShowDialog() == true)
            {
                _productService.Update(clonedProduct);
                var index = Products.IndexOf(SelectedProduct);
                Products[index] = clonedProduct;
            }
        }

        private void DeleteProduct()
        {
            if (SelectedProduct == null) return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá sản phẩm {SelectedProduct.ProductID}?", "Xác nhận xoá", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _productService.Delete(SelectedProduct.ProductID);
                Products.Remove(SelectedProduct);
            }
        }

        private void SearchProduct()
        {
            var results = _productService.Search(SearchKeyword ?? string.Empty);
            Products.Clear();
            foreach (var product in results)
            {
                Products.Add(product);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}