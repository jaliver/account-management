using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using FluentAssertions;

namespace CustomerService.Api.Test.Controllers
{
    public class CustomerControllerTest
    {
        private const string BaseUri = "api/v1/customerservice";

        [Test]
        public async Task When_CallingCreateCustomer_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetCreateCustomerEndpointUri("First Last");

            var response = await httpClient.PostAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingGetCustomer_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetCustomerEndpointUri("First Last");

            var response = await httpClient.GetAsync(requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static string GetCreateCustomerEndpointUri(string fullName)
        {
            return $"{BaseUri}/customer/{fullName}";
        }

        private static string GetCustomerEndpointUri(string fullName)
        {
            return $"{BaseUri}/customer/{fullName}";
        }
    }
}
