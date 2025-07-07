using BusinessObjects;

namespace Repositories.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetOrdersByCustomerID(int customerId);
        List<Order> GetAll();
        void Add(Order order);
        void Delete(int orderId);
        void Update(Order order);
        List<Order> Search(int? customerId, int? employeeId);
        List<OrderReportModel> GetReportByPeriod(DateTime startDate, DateTime endDate);
    }
}