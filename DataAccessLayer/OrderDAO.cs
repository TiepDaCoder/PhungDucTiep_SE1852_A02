using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        private readonly LucySalesDataContext _context;

        public OrderDAO()
        {
            _context = new LucySalesDataContext();
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .ToList();
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderID == orderId);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.OrderID == order.OrderID);
            if (existingOrder != null)
            {
                existingOrder.CustomerID = order.CustomerID;
                existingOrder.EmployeeID = order.EmployeeID;
                existingOrder.OrderDate = order.OrderDate;

                _context.SaveChanges();
            }
        }

        public List<Order> SearchOrders(int? customerId, int? employeeId)
        {
            var query = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (customerId.HasValue)
                query = query.Where(o => o.CustomerID == customerId.Value);

            if (employeeId.HasValue)
                query = query.Where(o => o.EmployeeID == employeeId.Value);

            return query.ToList();
        }

        public List<Order> GetOrdersByCustomerID(int customerId)
        {
            return _context.Orders
                .Where(o => o.CustomerID == customerId)
                .Include(o => o.OrderDetails)
                .ToList();
        }

        public List<OrderReportModel> GetReportByPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Include(o => o.OrderDetails)
                .AsEnumerable()
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new OrderReportModel
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalOrders = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderDetails.Sum(d =>
                        (double)(d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount))
                    ))
                })
                .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .ToList();
        }
    }
}