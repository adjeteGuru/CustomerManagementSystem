using CustomerManagementSystem.Core.DTOs;
using CustomerManagementSystem.Core.Models;

namespace CustomerManagementSystem.Core.Providers
{
    public interface ICustomersDataProvider
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> RemoveCustomerAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerAddDTo customer);
        Task<Customer> UpdateCustomerAsync(CustomerUpdateDto customer);
    }
}
