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
    }
}
