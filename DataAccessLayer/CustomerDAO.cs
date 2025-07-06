using BusinessObjects;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private readonly LucySalesDataContext _context;

        public CustomerDAO()
        {
            _context = new LucySalesDataContext();
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            return _context.Customers.FirstOrDefault(c => c.Phone == phone);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public bool UpdateCustomer(Customer customer)
        {
            var existing = _context.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
            if (existing == null) return false;

            existing.CompanyName = customer.CompanyName;
            existing.ContactName = customer.ContactName;
            existing.ContactTitle = customer.ContactTitle;
            existing.Address = customer.Address;
            existing.Phone = customer.Phone;

            _context.SaveChanges();
            return true;
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return _context.Customers
                .Where(c =>
                    c.CompanyName.Contains(keyword) ||
                    c.ContactName.Contains(keyword) ||
                    c.Phone.Contains(keyword))
                .ToList();
        }
    }
}