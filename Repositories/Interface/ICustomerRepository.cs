using BusinessObjects;

namespace Repositories.Interface
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
        Customer? GetCustomerAccount(string phone);
        bool UpdateCustomer(Customer customer);
        void Add(Customer customer);
        void Delete(int id);
        List<Customer> Search(string keyword);
    }
}