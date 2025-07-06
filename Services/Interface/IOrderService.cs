using BusinessObjects;

namespace Services.Interface
{
    public interface IOrderService
    {
        List<Order> GetOrdersForCustomer(int customerId);
        List<Order> GetAllOrders();
        List<Order> SearchOrders(int? customerId, int? employeeId);
        void AddOrder(Order order);
        void DeleteOrder(int orderId);

        List<OrderDetail> GetAllDetails();
        List<OrderDetail> GetDetailsByOrderId(int orderId);
        void AddOrderDetail(OrderDetail detail);
        void DeleteOrderDetailsByOrderId(int orderId);
        List<OrderReportModel> GetReportByPeriod(DateTime startDate, DateTime endDate);
    }
}