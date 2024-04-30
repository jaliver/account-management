using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using CustomerService.Api.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;
using CustomerService.Api.Models;

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
            
            var requestUrl = GetCreateCustomerEndpointUrl("John Doe");

            var response = await httpClient.PostAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Given_ExistingCustomer_When_CallingCreateCustomer_Then_ReturnsBadRequest()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();
            
            var requestUrl = GetCreateCustomerEndpointUrl("John Doe");
            await httpClient.PostAsync(requestUrl, null);

            var response = await httpClient.PostAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_ExistingCustomer_When_CallingGetCustomer_Then_ReturnsCustomer()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var fullName = "John Doe";
            await CreateCustomer(httpClient, fullName);

            var requestUrl = GetCustomerEndpointUrl(fullName);

            var response = await httpClient.GetAsync(requestUrl);
            var result = await response.Content.ReadFromJsonAsync<Customer>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().NotBeNull();
            result?.Name.Should().Be(fullName);
        }

        [Test]
        public async Task Given_NotExistingCustomer_When_CallingGetCustomer_Then_ReturnsNotFound()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var requestUrl = GetCustomerEndpointUrl("John Doe");

            var response = await httpClient.GetAsync(requestUrl);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static string GetCreateCustomerEndpointUrl(string fullName)
        {
            return $"{BaseUri}/customer/{fullName}";
        }

        private static string GetCustomerEndpointUrl(string fullName)
        {
            return $"{BaseUri}/customer/{fullName}";
        }

        // TODO change this to instantiating another in-memory db with data
        private async Task CreateCustomer(HttpClient httpClient, string fullName)
        {
            var requestUrl = GetCreateCustomerEndpointUrl(fullName);
            await httpClient.PostAsync(requestUrl, null);
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
