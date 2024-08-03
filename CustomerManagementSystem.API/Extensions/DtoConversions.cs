using CustomerManagementSystem.Infrastructure.DTOs;
using CustomerManagementSystem.Infrastructure.Models;

namespace CustomerManagementSystem.API.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<CustomerRead> ConvertToDto(this IEnumerable<Customer> customers, IEnumerable<Department> departments)
        {
            return (from customer in customers
            join department in departments
            on customer.DepartmentId equals department.Id
            select new CustomerRead()
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                PostCode = customer.PostCode,
                Telephone = customer.Telephone,
                DepartmentId = department.Id,
                DepartmentName = department.Name,
            }).ToList();
        }

        public static Customer ConvertToDto(this CustomerAddDTo customerAdd, Department department)
        {
            return new Customer
            {
                FullName = customerAdd.FullName,
                Address = customerAdd.Address,
                PostCode = customerAdd.PostCode,
                Telephone = customerAdd.Telephone,
                DepartmentId = department.Id
            };
        }

        public static CustomerRead ConvertToDto(this Customer customerAdd, Department department)
        {
            return new CustomerRead
            {
                FullName = customerAdd.FullName,
                Address = customerAdd.Address,
                PostCode = customerAdd.PostCode,
                Telephone = customerAdd.Telephone,
                DepartmentId = department.Id, 
                DepartmentName = department.Name
            };
        }
    }
}
