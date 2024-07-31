﻿using CustomerManagementSystem.Infrastructure.Models;

namespace CustomerManagementSystem.Infrastructure.Services
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task DeleteCustomerAsync(int id);
    }
}