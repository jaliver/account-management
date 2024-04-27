using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using FluentAssertions;

namespace AccountManagement.Api.Test.Controllers
{
    [TestFixture]
    public class SavingsAccountControllerTest
    {
        private const string BaseUri = "api/v1/savingsaccount";

        [Test]
        public async Task When_CallingCreateSavingsAccount_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();
            
            var requestUri = GetCreateSavingsAccountEndpointUri("First", "Last");

            var response = await httpClient.PostAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingDeposit_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetDepositEndpointUri(3, 100);

            var response = await httpClient.PutAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingWithdraw_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetWithdrawEndpointUri(3, 100);

            var response = await httpClient.PutAsync(requestUri, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingGetSavingsAccountBalance_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetSavingsAccountBalanceEndpointUri(3);

            var response = await httpClient.GetAsync(requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_CallingGetTransactions_Then_ReturnsOk()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var httpClient = app.CreateClient();

            var requestUri = GetTransactionsEndpointUri(3, 10);

            var response = await httpClient.GetAsync(requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static string GetCreateSavingsAccountEndpointUri(string customerFirstName, string customerSurname)
        {
            return $"{BaseUri}/savings-account?customerFirstName={customerFirstName}&customerSurname={customerSurname}";
        }

        private static string GetDepositEndpointUri(int accountNumber, decimal amount)
        {
            return $"{BaseUri}/deposit?accountNumber={accountNumber}&amount={amount}";
        }

        private static string GetWithdrawEndpointUri(int accountNumber, decimal amount)
        {
            return $"{BaseUri}/withdrawal?accountNumber={accountNumber}&amount={amount}";
        }

        private static string GetSavingsAccountBalanceEndpointUri(int accountNumber)
        {
            return $"{BaseUri}/savings-account-balance?accountNumber={accountNumber}";
        }

        private static string GetTransactionsEndpointUri(int accountNumber, int numberOfTransactions)
        {
            return $"{BaseUri}/transactions?accountNumber={accountNumber}&numberOfTransactions={numberOfTransactions}";
        }
    }
}
