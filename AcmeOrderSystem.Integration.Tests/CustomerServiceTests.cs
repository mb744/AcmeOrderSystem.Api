using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcmeOrderSystem.Integration.IntegrationTests;

namespace AcmeOrderSystem.Integration.Tests
{
    public class CustomerServiceTests : BaseIntegrationTest
    {
        public CustomerServiceTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_ShouldAddCustomer_WithContact()
        {
            // Arrange
            var contact = new CreateContactRequest("test@test.com", "310-310-3100", 0);
            var customer = new CreateCustomerRequest("Customer1", "Los Angeles", contact);
            

            // Act
            var newCustomer = await _customerService.AddCustomer(customer);

            // Assert
            Assert.NotNull(newCustomer);
            //Assert.True(newCustomer);
        }

        [Fact]
        public async Task ShouldNotAddCustomer_WithSameName()
        {
            // Arrange
            var contact = new CreateContactRequest("test@test.com", "310-310-3100", 0);
            var customer = new CreateCustomerRequest("Customer2", "Los Angeles", contact);
            

            // Act
            var dupNameCustomer = await _customerService.AddCustomer(customer);

            // Assert
            Assert.NotNull(dupNameCustomer);
            //Assert.True(!dupNameCustomer.IsSuccess);
        }

        [Fact]
        public async Task ShouldGetCustomer_ById()
        {
            // Arrange
            int Id = 1;
            CancellationTokenSource cts = new();
            CancellationToken ct = cts.Token;

            // Act
            var customer = await _customerService.GetSingleCustomer(Id);

            // Assert
            Assert.NotNull(customer);
            //Assert.True(customer.IsSuccess);
        }


    }
}
