using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using CustomerService.Api.Data;
using CustomerService.Api.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CustomerService.Api.Test.Controllers
{
    public class CustomerServiceControllerTest
    {
        private const string BaseUri = "api/v1/customerservice";

        [Test]
        public async Task Given_NewCustomer_When_CallingCreateCustomer_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();
            
            var requestUri = GetCreateCustomerEndpointUri("John Doe", 3);

            var response = await httpClient.PostAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Given_ExistingCustomer_When_CallingCreateCustomer_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customer = "John Doe";
            var requestUri = GetCreateCustomerEndpointUri(customer, 3);
            await httpClient.PostAsync(requestUri, null);

            var response = await httpClient.PostAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_ExistingCustomer_When_CallingGetCustomer_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customer = new Customer
            {
                Name = "John Doe",
                SavingsAccountId = 3
            };

            await CreateCustomer(httpClient, customer.Name, customer.SavingsAccountId);

            var requestUri = GetCustomerEndpointUri(customer.Name);

            var response = await httpClient.GetAsync(requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Given_NotExistingCustomer_When_CallingGetCustomer_Then_ReturnsNotFound()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customer = new Customer
            {
                Name = "John Doe",
                SavingsAccountId = 3
            };

            var requestUri = GetCustomerEndpointUri(customer.Name);

            var response = await httpClient.GetAsync(requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static string GetCreateCustomerEndpointUri(string fullName, int savingsAccountId)
        {
            return $"{BaseUri}/customer/{fullName}/{savingsAccountId}";
        }

        private static string GetCustomerEndpointUri(string fullName)
        {
            return $"{BaseUri}/customer/{fullName}";
        }

        // TODO change this to instantiating another in-memory db with data
        private async Task CreateCustomer(HttpClient httpClient, string fullName, int savingsAccountId)
        {
            var requestUri = GetCreateCustomerEndpointUri(fullName, savingsAccountId);
            await httpClient.PostAsync(requestUri, null);
        }

        private static async Task<WebApplicationFactory<Program>> GetWebAppFactory(string uniqueTestDbName)
        {
            await using var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<CustomerDbContext>));
                        services.AddDbContext<CustomerDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(uniqueTestDbName);
                        });
                    }));

            return app;
        }
    }
}
