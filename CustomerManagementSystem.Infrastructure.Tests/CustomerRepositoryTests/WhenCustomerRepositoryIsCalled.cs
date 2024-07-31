using CustomerManagementSystem.Infrastructure.Database;
using CustomerManagementSystem.Infrastructure.Models;
using CustomerManagementSystem.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Infrastructure.Tests.CustomerRepositoryTests
{
    public class WhenCustomerRepositoryIsCalled
    {
        private int dbCount;

        [Fact]
        public void Constructor_WhenCalledWithNullContext_ThenExpectedErrorIsThrown()
        {
            var act = ()=> new CustomerRepository(null);
            act.Should().Throw<ArgumentNullException>("test");
        }

        [Fact]
        public async Task AndValidDataIsReturned_ThenTheExpectedResultIsReturned()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    FullName = "John Doe",
                    Address = "123 Rad road",
                    PostCode = "DE12 6AS",
                    Telephone = "01151234567"
                },
                new Customer
                {
                    Id = 2,
                    FullName = "John Smith",
                    Address = "456 Well Avenue",
                    PostCode = "BS6 6QW",
                    Telephone = "01151234789"
                }
            };
            await using (GetContext())
            {
                var context = GetContext();
                context.Customers.AddRange(customers);
                await context.SaveChangesAsync();

                var systemUnderTest = new CustomerRepository(context);

                var result = await systemUnderTest.GetAllCustomersAsync();
                result.Should().BeEquivalentTo(customers);
            }
        }

        [Fact]
        public async Task AndThereIsNoValidDataReturned_ThenTheExpectedErrorIsReturned()
        {
            var invalidData = new List<Customer>();
            await using (GetContext())
            {
                //var context = GetContext();
                GetContext().Customers.AddRange(invalidData);
                await GetContext().SaveChangesAsync();

                var systemUnderTest = new CustomerRepository(GetContext());

                Func<Task> func = async () => await systemUnderTest.GetAllCustomersAsync();
                await func.Should().ThrowAsync<Exception>().WithMessage("no customer found in the db!");
            }
        }

        private WebAppContext GetContext()
        {
            dbCount++;
            var options = new DbContextOptionsBuilder<WebAppContext>()
                .UseInMemoryDatabase(databaseName: $"WebApp{dbCount}")
                .Options;
            return new WebAppContext(options);
        }
    }
}
