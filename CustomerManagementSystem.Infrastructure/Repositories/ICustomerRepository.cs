using CustomerManagementSystem.Infrastructure.Models;

namespace CustomerManagementSystem.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task DeleteCustomerAsync(int id);
        bool SaveChanges();
        Task<Department> GetDepartmentByCustomerIdAsync(int customerId);
        Task<Department> GetDepartmentByCustomerDepartNameAsync(string departName);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    }
}
