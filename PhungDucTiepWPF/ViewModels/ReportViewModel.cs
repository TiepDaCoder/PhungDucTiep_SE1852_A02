using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using BusinessObjects;
using PhungDucTiepWPF.Commands;
using Services.Implementation;

public class ReportViewModel : INotifyPropertyChanged
{
    private readonly OrderService _orderService = new();

    private DateTime _startDate = DateTime.Now.AddMonths(-1);
    private DateTime _endDate = DateTime.Now;

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    public ObservableCollection<OrderReportModel> Reports { get; set; }

    public ICommand GenerateReportCommand { get; }

    public ReportViewModel()
    {
        Reports = new ObservableCollection<OrderReportModel>();
        GenerateReportCommand = new RelayCommand(GenerateReport);
        GenerateReport(); // Load mặc định lúc khởi chạy
    }

    private void GenerateReport()
    {
        Reports.Clear();
        var data = _orderService.GetReportByPeriod(StartDate, EndDate);
        foreach (var item in data)
        {
            Reports.Add(item);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}