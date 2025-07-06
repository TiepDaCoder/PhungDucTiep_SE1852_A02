using BusinessObjects;
using Repositories.Implementation;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public Employee? Login(string username, string password)
        {
            return _employeeRepository.GetEmployeeAccount(username, password);
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public void UpdateProfile(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }
    }
}