using BusinessObjects;
using Repositories.Implementation;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public Customer? Login(string phone)
        {
            return _customerRepository.GetCustomerAccount(phone);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }

        public List<Customer> GetAll() => _customerRepository.GetAllCustomers();

        public void Add(Customer customer) => _customerRepository.Add(customer);

        public void Delete(int id) => _customerRepository.Delete(id);

        public List<Customer> Search(string keyword) => _customerRepository.Search(keyword);
    }
}