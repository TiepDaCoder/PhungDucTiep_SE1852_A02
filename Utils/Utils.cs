using BusinessObjects;

namespace Utils
{
    public class IDGenerate
    {
        public static int OrderIDGenerator(List<Order> list)
        {
            int max = 0;
            foreach (var order in list)
            {
                if (max <= order.OrderID)
                {
                    max = order.OrderID;
                }
            }
            max++;
            return max;
        }
    }
}