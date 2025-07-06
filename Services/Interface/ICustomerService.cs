using BusinessObjects;

namespace Services.Interface
{
    public interface ICustomerService
    {
        Customer? Login(string phone);
        bool UpdateCustomer(Customer customer);
        List<Customer> GetAll();
        void Add(Customer customer);
        void Delete(int id);
        List<Customer> Search(string keyword);
    }
}