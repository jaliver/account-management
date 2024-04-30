using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using FluentAssertions;

namespace AccountManagement.Api.Test.Controllers
{
    public class AccountManagementControllerTest
    {
        private const string BaseUri = "api/v1/accountmanagement";

        [Test]
        public async Task When_CallingCreateSavingsAccount_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();
            
            var requestUri = GetCreateSavingsAccountEndpointUri("John Doe");

            var response = await httpClient.PostAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingDeposit_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetDepositEndpointUri("John Doe", 100);

            var response = await httpClient.PutAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingWithdraw_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetWithdrawEndpointUri("John Doe", 100);

            var response = await httpClient.PutAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingGetSavingsAccountBalance_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetSavingsAccountBalanceEndpointUri("John Doe");

            var response = await httpClient.GetAsync(requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingGetTransactions_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetTransactionsEndpointUri("John Doe", 10);

            var response = await httpClient.GetAsync(requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static string GetCreateSavingsAccountEndpointUri(string customerFullName)
        {
            return $"{BaseUri}/savings-account/{customerFullName}";
        }

        private static string GetDepositEndpointUri(string customerFullName, double amount)
        {
            return $"{BaseUri}/deposit/{customerFullName}/{amount}";
        }

        private static string GetWithdrawEndpointUri(string customerFullName, double amount)
        {
            return $"{BaseUri}/withdrawal/{customerFullName}/{amount}";
        }

        private static string GetSavingsAccountBalanceEndpointUri(string customerFullName)
        {
            return $"{BaseUri}/savings-account-balance/{customerFullName}";
        }

        private static string GetTransactionsEndpointUri(string customerFullName, int numberOfTransactions)
        {
            return $"{BaseUri}/transactions/{customerFullName}/{numberOfTransactions}";
        }
    }
}
