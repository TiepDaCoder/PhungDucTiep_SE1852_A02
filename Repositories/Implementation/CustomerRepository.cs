using BusinessObjects;
using DataAccessLayer;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDAO _customerDAO;

        public CustomerRepository()
        {
            _customerDAO = new CustomerDAO();
        }

        public List<Customer> GetAllCustomers() => _customerDAO.GetAllCustomers();

        public Customer? GetCustomerAccount(string phone) => _customerDAO.GetCustomerByPhone(phone);

        public bool UpdateCustomer(Customer customer) => _customerDAO.UpdateCustomer(customer);

        public void Add(Customer customer) => _customerDAO.AddCustomer(customer);

        public void Delete(int id) => _customerDAO.DeleteCustomer(id);

        public List<Customer> Search(string keyword) => _customerDAO.SearchCustomers(keyword);
    }
}