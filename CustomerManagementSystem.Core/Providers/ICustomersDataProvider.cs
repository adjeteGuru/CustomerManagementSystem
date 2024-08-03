using CustomerManagementSystem.Core.DTOs;
using CustomerManagementSystem.Core.Models;

namespace CustomerManagementSystem.Core.Providers
{
    public interface ICustomersDataProvider
    {
        Task<IEnumerable<CustomerRead>> GetAllCustomersAsync();
        Task<CustomerRead> GetCustomerByIdAsync(int id);
        Task<CustomerRead> RemoveCustomerAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerAddDTo customer);
        Task<Customer> UpdateCustomerAsync(CustomerUpdateDto customer);
    }
}
