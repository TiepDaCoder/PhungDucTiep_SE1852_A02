using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;

namespace PhungDucTiepWPF.ViewModels
{
    public class ProductDialogViewModel : INotifyPropertyChanged
    {
        public Product Product { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action? RequestCloseWithSuccess;
        public event Action? RequestClose;

        public ProductDialogViewModel(Product product)
        {
            Product = product;
            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnSave()
        {
            RequestCloseWithSuccess?.Invoke();
        }

        private void OnCancel()
        {
            RequestClose?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}