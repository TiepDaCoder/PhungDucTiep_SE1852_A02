using BusinessObjects;
using Services.Implementation;

namespace PhungDucTiepWPF.ViewModels
{
    public class OrderHistoryViewModel
    {
        public List<Order> Orders { get; set; }

        public OrderHistoryViewModel(int customerId)
        {
            var orderService = new OrderService();
            Orders = orderService.GetOrdersForCustomer(customerId);
        }
    }
}