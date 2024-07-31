using CustomerManagementSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagementSystem.Infrastructure.Database
{
    public class DbInitializer
    {
        public static void EnsureSeedData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<WebAppContext>();
            context.Database.EnsureCreated();

            if (context.Customers != null && context.Customers.Any() || context.Departments != null && context.Departments.Any()) 
                return;
           
            var departments = GetDepartments().ToArray();
            context.Departments.AddRange(departments);
            context.SaveChanges();
            var customers = GetCustomers().ToArray();
            context.Customers.AddRange(customers);            
            

            context.SaveChanges();
        }

        public static List<Department> GetDepartments()
        {
            var departments = new List<Department>
            {
                new Department
                {
                    Name = "HR"
                },
                new Department
                {
                    Name = "IT"
                },
                new Department
                {
                    Name = "Support"
                },
                new Department
                {
                    Name = "Welfare"
                }
            };

            return departments;
        }

        public static List<Customer> GetCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    FullName = "John Doe",
                    Address = "123 Rad road",
                    PostCode = "DE12 6AS",
                    Telephone = "01151234567",
                    DepartmentId = 1
                },
                new Customer
                {
                    FullName = "John Smith",
                    Address = "456 Well Avenue",
                    PostCode = "BS6 6QW",
                    Telephone = "01151234789",
                    DepartmentId = 2
                },
                new Customer
                {
                    FullName = "Andy Doe",
                    Address = "33 Nem road",
                    PostCode = "DE12 6AS",
                    Telephone = "01151234567",
                    DepartmentId = 3
                },
                new Customer
                {
                    FullName = "Tom Smith",
                    Address = "12A Well Avenue",
                    PostCode = "BS6 6QW",
                    Telephone = "01151234789",
                    DepartmentId = 4
                },
                new Customer
                {
                    FullName = "Bryan Bob",
                    Address = "123B Rad road",
                    PostCode = "DE12 6AS",
                    Telephone = "01151234567",
                    DepartmentId = 1
                },
                new Customer
                {
                    FullName = "Kate Nate",
                    Address = "456 Torn Avenue",
                    PostCode = "BS6 6QW",
                    Telephone = "01151234789",
                    DepartmentId = 2
                },
                new Customer
                {
                    FullName = "Ekua Johnson",
                    Address = "149 Broxow road",
                    PostCode = "DE12 6AS",
                    Telephone = "01151234567",
                    DepartmentId = 3
                },
                new Customer
                {
                    FullName = "Elton Dave",
                    Address = "33A Bell Avenue",
                    PostCode = "BS6 6QW",
                    Telephone = "01151234789",
                    DepartmentId = 4
                },
                new Customer
                {
                    FullName = "Justin Sam",
                    Address = "123 Radford road",
                    PostCode = "DE12 6AS",
                    Telephone = "01151234567",
                    DepartmentId = 1
                },
                new Customer
                {
                    FullName = "Scottie Adjevi",
                    Address = "456 Bee Avenue",
                    PostCode = "BS6 6QW",
                    Telephone = "01151234789",
                    DepartmentId = 3
                }
            };

            return customers;
        }
    }
}
