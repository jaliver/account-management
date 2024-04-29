using System.Net;
using AccountService.Api.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NUnit.Framework;

namespace AccountService.Api.Test.Controllers
{
    public class AccountServiceControllerTest
    {
        private const string BaseUri = "api/v1/accountservice";

        [Test]
        public async Task Given_CustomerWithoutSavingsAccount_When_CallingCreateSavingsAccount_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var requestUrl = GetCreateSavingsAccountEndpointUrl(5);

            var response = await httpClient.PostAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Given_CustomerWithExistingSavingsAccount_When_CallingCreateSavingsAccount_Then_ReturnsBadRequest()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customerId = 5;
            await CreateSavingsAccount(httpClient, customerId);
            var requestUrl = GetCreateSavingsAccountEndpointUrl(customerId);

            var response = await httpClient.PostAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_ExistingSavingsAccount_When_CallingGetSavingsAccount_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customerId = 5;
            await CreateSavingsAccount(httpClient, customerId);
            var requestUrl = GetSavingsAccountEndpointUrl(customerId);

            var response = await httpClient.GetAsync(requestUrl);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Given_NotExistingSavingsAccount_When_CallingGetSavingsAccount_Then_ReturnsNotFound()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();
            
            var requestUrl = GetSavingsAccountEndpointUrl(5);

            var response = await httpClient.GetAsync(requestUrl);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Given_ExistingSavingsAccountAndPositiveAmountToDeposit_When_CallingDepositToSavingsAccount_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customerId = 5;
            var amountToDeposit = 1;
            await CreateSavingsAccount(httpClient, customerId);

            var requestUrl = GetDepositToSavingsAccountEndpointUrl(customerId, amountToDeposit);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Given_ExistingSavingsAccountAndNegativeAmountToDeposit_When_CallingDepositToSavingsAccount_Then_ReturnsBadRequest()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();
            
            var customerId = 5;
            var amountToDeposit = -1;
            await CreateSavingsAccount(httpClient, customerId);

            var requestUrl = GetDepositToSavingsAccountEndpointUrl(customerId, amountToDeposit);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_NotExistingSavingsAccountAndPositiveAmountToDeposit_When_CallingDepositToSavingsAccount_Then_ReturnsBadRequest()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();
            
            var amountToDeposit = 1;

            var requestUrl = GetDepositToSavingsAccountEndpointUrl(5, amountToDeposit);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_NotExistingSavingsAccountAndNegativeAmountToDeposit_When_CallingDepositToSavingsAccount_Then_ReturnsBadRequest()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var amountToDeposit = -1;

            var requestUrl = GetDepositToSavingsAccountEndpointUrl(5, amountToDeposit);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_ExistingSavingsAccountAndPositiveAmountToWithdraw_When_CallingWithdrawFromSavingsAccount_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customerId = 5;
            var amountToWithdraw = 1;
            await CreateSavingsAccount(httpClient, customerId);

            var requestUrl = GetWithdrawFromSavingsAccountEndpointUrl(customerId, amountToWithdraw);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Given_ExistingSavingsAccountAndNegativeAmountToWithdraw_When_CallingWithdrawFromSavingsAccount_Then_ReturnsBadRequest()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var customerId = 5;
            var amountToWithdraw = -1;
            await CreateSavingsAccount(httpClient, customerId);

            var requestUrl = GetWithdrawFromSavingsAccountEndpointUrl(customerId, amountToWithdraw);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_NotExistingSavingsAccountAndPositiveAmountToWithdraw_When_CallingWithdrawFromSavingsAccount_Then_ReturnsOk()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();
            
            var amountToWithdraw = 1;

            var requestUrl = GetWithdrawFromSavingsAccountEndpointUrl(5, amountToWithdraw);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_NotExistingSavingsAccountAndNegativeAmountToWithdraw_When_CallingWithdrawFromSavingsAccount_Then_ReturnsBadRequest()
        {
            var app = await GetWebAppFactory(Guid.NewGuid().ToString());
            using var httpClient = app.CreateClient();

            var amountToWithdraw = -1;

            var requestUrl = GetWithdrawFromSavingsAccountEndpointUrl(5, amountToWithdraw);

            var response = await httpClient.PutAsync(requestUrl, null);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private static string GetCreateSavingsAccountEndpointUrl(int customerId)
        {
            return $"{BaseUri}/savings-account/{customerId}";
        }

        private static string GetSavingsAccountEndpointUrl(int customerId)
        {
            return $"{BaseUri}/savings-account/{customerId}";
        }

        private static string GetDepositToSavingsAccountEndpointUrl(int customerId, decimal amount)
        {
            return $"{BaseUri}/deposit-to-savings-account/{customerId}/{amount}";
        }

        private static string GetWithdrawFromSavingsAccountEndpointUrl(int customerId, decimal amount)
        {
            return $"{BaseUri}/withdraw-from-savings-account/{customerId}/{amount}";
        }

        // TODO change this to instantiating another in-memory db with data
        private static async Task CreateSavingsAccount(HttpClient httpClient, int customerId)
        {
            var requestUrl = GetCreateSavingsAccountEndpointUrl(customerId);
            await httpClient.PostAsync(requestUrl, null);
        }

        private static async Task<WebApplicationFactory<Program>> GetWebAppFactory(string uniqueTestDbName)
        {
            await using var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<AccountDbContext>));
                        services.AddDbContext<AccountDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(uniqueTestDbName);
                        });
                    }));

            return app;
        }
    }
}
