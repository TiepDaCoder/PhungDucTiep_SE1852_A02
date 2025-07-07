using BusinessObjects;
using DataAccessLayer;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDAO _orderDAO;

        public OrderRepository()
        {
            _orderDAO = new OrderDAO();
        }

        public List<Order> GetOrdersByCustomerID(int customerId)
        {
            return _orderDAO.GetOrdersByCustomerID(customerId);
        }

        public List<Order> GetAll() => _orderDAO.GetAllOrders();

        public void Add(Order order) => _orderDAO.AddOrder(order);

        public void Delete(int orderId) => _orderDAO.DeleteOrder(orderId);

        public List<Order> Search(int? customerId, int? employeeId) =>
            _orderDAO.SearchOrders(customerId, employeeId);

        public List<OrderReportModel> GetReportByPeriod(DateTime startDate, DateTime endDate)
        {
            return new OrderDAO().GetReportByPeriod(startDate, endDate);
        }

        public void Update(Order order) => _orderDAO.UpdateOrder(order);
    }
}