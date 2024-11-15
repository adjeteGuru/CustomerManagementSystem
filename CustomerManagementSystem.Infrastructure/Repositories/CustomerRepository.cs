﻿using CustomerManagementSystem.Infrastructure.Database;
using CustomerManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly WebAppContext _context;

        public CustomerRepository(WebAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException("customer must not be null!");
            }

            var isCustomerExist = CustomerExistCheck(customer.FullName);

            if (isCustomerExist)
            {
                throw new Exception("name already exist in the db!");
            }

            _context.Customers.Add(customer);

            SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException("customer must not be null!");
            }

            _context.Customers.Update(customer);

            SaveChanges();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();          
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                throw new Exception("name not found in the db!");
            }
            return customer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await GetCustomerByIdAsync(id);

            if (customer == null)
            {
                throw new Exception("name not found in the db!");
            }

            _context.Customers.Remove(customer);

            SaveChanges();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        private bool CustomerExistCheck(string name)
        {
            return _context.Customers.Any(c => c.FullName.ToLower().Equals(name.ToLower()));
        }      

        public async Task<Department> GetDepartmentByCustomerDepartNameAsync(string departName)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(departName.ToLower().Trim()));         
            return department;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Department> GetDepartmentByCustomerIdAsync(int customerId)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id.Equals(customerId));
            return department;
        }
    }
}
