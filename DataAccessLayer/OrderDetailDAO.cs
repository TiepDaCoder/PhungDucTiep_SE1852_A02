using BusinessObjects;

namespace DataAccessLayer
{
    public class OrderDetailDAO
    {
        private readonly LucySalesDataContext _context;

        public OrderDetailDAO()
        {
            _context = new LucySalesDataContext();
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return _context.OrderDetails.ToList();
        }

        public List<OrderDetail> GetDetailsByOrderId(int orderId)
        {
            return _context.OrderDetails
                .Where(d => d.OrderID == orderId)
                .ToList();
        }

        public void AddOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);
            _context.SaveChanges();
        }

        public void DeleteOrderDetailsByOrderId(int orderId)
        {
            var details = _context.OrderDetails.Where(d => d.OrderID == orderId).ToList();
            _context.OrderDetails.RemoveRange(details);
            _context.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail detail)
        {
            var existingDetail = _context.OrderDetails
                .FirstOrDefault(d => d.OrderID == detail.OrderID && d.ProductID == detail.ProductID);

            if (existingDetail != null)
            {
                existingDetail.UnitPrice = detail.UnitPrice;
                existingDetail.Quantity = detail.Quantity;
                existingDetail.Discount = detail.Discount;

                _context.SaveChanges();
            }
        }
    }
}