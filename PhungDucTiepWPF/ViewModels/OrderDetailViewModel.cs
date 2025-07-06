using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BusinessObjects;

namespace PhungDucTiepWPF.ViewModels
{
    public class OrderDetailViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> AvailableProducts { get; }

        private Product _product;
        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                ProductID = value?.ProductID ?? 0;
                UnitPrice = (value?.UnitPrice ?? 0);
                OnPropertyChanged();
                OnPropertyChanged(nameof(Product));
            }
        }

        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public OrderDetailViewModel(ObservableCollection<Product> products)
        {
            AvailableProducts = products;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}