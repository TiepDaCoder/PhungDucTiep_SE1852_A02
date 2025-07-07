using BusinessObjects;

namespace Repositories.Interface
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetAll();
        List<OrderDetail> GetByOrderId(int orderId);
        void Add(OrderDetail detail);
        void DeleteDetailsByOrderId(int orderId);
        void Update(OrderDetail detail);
    }
}