using BusinessObjects;
using DataAccessLayer;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderDetailDAO _orderDetailDAO;

        public OrderDetailRepository()
        {
            _orderDetailDAO = new OrderDetailDAO();
        }

        public List<OrderDetail> GetAll() => _orderDetailDAO.GetAllOrderDetails();

        public List<OrderDetail> GetByOrderId(int orderId) =>
            _orderDetailDAO.GetDetailsByOrderId(orderId);

        public void Add(OrderDetail detail) =>
            _orderDetailDAO.AddOrderDetail(detail);

        public void DeleteDetailsByOrderId(int orderId) =>
            _orderDetailDAO.DeleteOrderDetailsByOrderId(orderId);

        public void Update(OrderDetail detail) =>
            _orderDetailDAO.UpdateOrderDetail(detail);
    }
}