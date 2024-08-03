using CustomerManagementSystem.Infrastructure.Models;
using CustomerManagementSystem.Infrastructure.Repositories;

namespace CustomerManagementSystem.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            return customer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _customerRepository.GetAllDepartmentsAsync();
        }

        public async Task<Department> GetDepartmentByCustomerDepartNameAsync(string departName)
        {
            return await _customerRepository.GetDepartmentByCustomerDepartNameAsync(departName);
        }

        public async Task<Department> GetDepartmentByCustomerIdAsync(int id)
        {
            return await _customerRepository.GetDepartmentByCustomerIdAsync(id);
        }
    }
}
