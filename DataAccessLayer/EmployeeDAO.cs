using BusinessObjects;

namespace DataAccessLayer
{
    public class EmployeeDAO
    {
        private readonly LucySalesDataContext _context;

        public EmployeeDAO()
        {
            _context = new LucySalesDataContext();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee? GetEmployeeAccount(string username, string password)
        {
            return _context.Employees
                .FirstOrDefault(e => e.UserName == username && e.Password == password);
        }

        public void UpdateEmployee(Employee employee)
        {
            var existing = _context.Employees.FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.UserName = employee.UserName;
                existing.Password = employee.Password;
                existing.JobTitle = employee.JobTitle;
                existing.BirthDate = employee.BirthDate;
                existing.HireDate = employee.HireDate;
                existing.Address = employee.Address;

                _context.SaveChanges();
            }
        }
    }
}