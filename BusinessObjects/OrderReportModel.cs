namespace BusinessObjects
{
    public class OrderReportModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalOrders { get; set; }
        public double TotalRevenue { get; set; }
    }
}