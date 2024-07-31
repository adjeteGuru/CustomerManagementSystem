using CustomerManagementSystem.Infrastructure.Models;
using CustomerManagementSystem.Infrastructure.Repositories;
using CustomerManagementSystem.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace CustomerManagementSystem.Infrastructure.Tests.CustomerServiceTests
{
    public class WhenCustomerServiceIsCalled
    {
        private readonly List<Customer> customers;
        private readonly Mock<ICustomerRepository> mockCustomerService;
        private readonly CustomerService systemUnderTest;

        public WhenCustomerServiceIsCalled()
        {
            customers = new List<Customer>
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

            mockCustomerService = new Mock<ICustomerRepository>();
            mockCustomerService.Setup(x => x.GetAllCustomersAsync())
                .ReturnsAsync(customers);
            systemUnderTest = new CustomerService(mockCustomerService.Object);
        }

        [Fact]
        public void Constructor_WhenCalledWithNullCustomerRepository_ThenExpectedErrorIsThrown()
        {
            var act = () => new CustomerService(null);
            act.Should().Throw<ArgumentNullException>("test");
        }

        [Fact]
        public async Task ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetAllCustomersAsync();
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public async Task AndValidDataIsReturned_ThenTheExpectedResultIsReturned()
        {
            var result = await systemUnderTest.GetAllCustomersAsync();
            result.Should().BeEquivalentTo(customers);
        }
    }
}
