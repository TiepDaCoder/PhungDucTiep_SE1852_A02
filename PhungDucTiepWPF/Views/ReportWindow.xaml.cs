using System.Windows;

namespace PhungDucTiepWPF.Views;
public partial class ReportWindow : Window
{
    public ReportWindow()
    {
        InitializeComponent();
        DataContext = new ReportViewModel();
    }
}