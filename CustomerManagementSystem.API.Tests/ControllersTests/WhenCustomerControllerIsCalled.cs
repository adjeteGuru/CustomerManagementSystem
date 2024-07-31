using AutoMapper;
using CustomerManagementSystem.API.Controllers;
using CustomerManagementSystem.Infrastructure.DTOs;
using CustomerManagementSystem.Infrastructure.Models;
using CustomerManagementSystem.Infrastructure.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Reflection;

namespace CustomerManagementSystem.API.Tests.ControllersTests
{
    public class WhenCustomerControllerIsCalled
    {
        private readonly List<Customer> customers;
        private readonly List<CustomerRead> expectedCustomers;
        private readonly Mock<ICustomerService> mockCustomerService;
        private readonly Mock<IMapper> mockIMapper;
        private readonly CustomerController systemUnderTest;

        public WhenCustomerControllerIsCalled()
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
            expectedCustomers = new List<CustomerRead>()
            {
                new CustomerRead
                {
                    Id = customers[0].Id,
                    FullName = customers[0].FullName,
                    Address = customers[0].Address,
                    PostCode = customers[0].PostCode,
                    Telephone = customers[0].Telephone
                }
            };

            mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(x => x.GetAllCustomersAsync())
                .ReturnsAsync(customers);

            mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(x => x
                    .Map<IEnumerable<CustomerRead>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(expectedCustomers);

            systemUnderTest = new CustomerController(mockCustomerService.Object, mockIMapper.Object);
        }

        [Fact]
        public void Constructor_WhenCalledWithNullCustomerService_ThenExpectedErrorIsThrown()
        {
            var act = () => new CustomerController(null, mockIMapper.Object);
            act.Should().Throw<ArgumentNullException>("test");
        }

        [Fact]
        public void Constructor_WhenCalledWithNullMapper_ThenExpectedErrorIsThrown()
        {
            var act = () => new CustomerController(mockCustomerService.Object,null);
            act.Should().Throw<ArgumentNullException>("test");
        }

        [Fact]
        public async Task ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetAllCustomersAsync();
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public void AndItHasHttpGetAttribute()
        {
            var attribute = typeof(CustomerController)
                .GetMethods()
                .Single(x => x.Name == nameof(CustomerController.GetAllCustomersAsync))
                .GetCustomAttributes(typeof(HttpGetAttribute)).Should().HaveCount(1);
        }

        [Fact]
        public async Task ThenCustomerServiceIsInvoked()
        {
            await systemUnderTest.GetAllCustomersAsync();

            mockCustomerService.Verify(x => x.GetAllCustomersAsync(), Times.Once);
        }

        [Fact]
        public async Task ThenAuToMapperIsInvoked()
        {
            await systemUnderTest.GetAllCustomersAsync();

            mockIMapper.Verify(x => x
                .Map<IEnumerable<CustomerRead>>(It.IsAny<IEnumerable<Customer>>()), Times.Once);
        }

        [Fact]
        public async Task ThenTheExpectedResponseTypeIsReturned()
        {
            var result = await systemUnderTest.GetAllCustomersAsync() as OkObjectResult;
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ThenTheExpectedResultIsReturned()
        {
            var result = await systemUnderTest.GetAllCustomersAsync();
            var okObjectResult = (OkObjectResult)result;

            var returnedCustomers = (IEnumerable<CustomerRead>)okObjectResult.Value!;
            returnedCustomers.Should().BeEquivalentTo(expectedCustomers);
        }

        [Fact]
        public async Task AndThereIsAnEmptyListCustomersThenNoFoundIsReturned()
        {
            mockCustomerService.Setup(x => x.GetAllCustomersAsync())
                .ReturnsAsync(new List<Customer>());

            var result = await systemUnderTest.GetAllCustomersAsync();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AndAnErrorOccuredThenTheExpectedExceptionIsReturned()
        {
            mockCustomerService.Setup(x => x.GetAllCustomersAsync())
                .ThrowsAsync(new Exception("test"));

            var result = await systemUnderTest.GetAllCustomersAsync();

            var objectResult = result as ObjectResult;
            objectResult!.StatusCode.Should().Be(500, "test");
        }
    }
}
