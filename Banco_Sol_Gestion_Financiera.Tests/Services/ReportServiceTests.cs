using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Entities;
using Banco_Sol_Gestion_Financiera.Enums;
using Banco_Sol_Gestion_Financiera.Services.ExchangeRate;
using Banco_Sol_Gestion_Financiera.Services.Reports;
using Banco_Sol_Gestion_Financiera.Tests.Helpers;
using Banco_Sol_Gestion_Financiera.Validators;
using Moq;

namespace Banco_Sol_Gestion_Financiera.Tests.Services
{
    public class ReportServiceTests
    {
        [Fact]
        public async Task GetBalance_ShouldReturnTotalInBOB()
        {
            // Arrange
            var context = DBContextFactory.Create();

            context.Users.Add(new User
            {
                Id = 1,
                Name = "Gabriel",
                Email = "gabriel@test.com",
                Password = "123"
            });

            context.Incomes.AddRange(
                new Income
                {
                    UserId = 1,
                    Amount = 3000,
                    Currency = CurrencyEnum.BOB,
                    Description = "",
                    Source = "",
                    ReceivedAt = new DateTime(2026, 1, 10)
                },
                new Income
                {
                    UserId = 1,
                    Amount = 100,
                    Currency = CurrencyEnum.USD,
                    Description = "",
                    Source = "",
                    ReceivedAt = new DateTime(2026, 1, 10)
                });

            await context.SaveChangesAsync();

            var exchangeRate = new Mock<IExchangeRateService>();

            exchangeRate
                .Setup(x => x.GetCurrentRateAsync())
                .ReturnsAsync(new ExchangeRateDto
                {
                    ExchangeRate = 6.92m
                });

            var service = new ReportService(
                context,
                exchangeRate.Object,
                new BalanceRequestDtoValidator());

            // Act
            var result = await service.GetBalanceAsync(
                1,
                new BalanceRequestDto
                {
                    StartDate = new DateTime(2026, 1, 1),
                    EndDate = new DateTime(2026, 7, 8),
                    Currency = CurrencyEnum.BOB
                });

            // Assert
            Assert.Equal(3692m, result.Total);
        }

        [Fact]
        public async Task GetBalance_ShouldReturnTotalInUSD()
        {
            // Arrange
            var context = DBContextFactory.Create();

            context.Users.Add(new User
            {
                Id = 1,
                Name = "Gabriel",
                Email = "gabriel@test.com",
                Password = "123"
            });

            context.Incomes.AddRange(
                new Income
                {
                    UserId = 1,
                    Amount = 6920,
                    Currency = CurrencyEnum.BOB,
                    Description = "",
                    Source = "",
                    ReceivedAt = new DateTime(2026, 1, 10)
                },
                new Income
                {
                    UserId = 1,
                    Amount = 200,
                    Currency = CurrencyEnum.USD,
                    Description = "",
                    Source = "",
                    ReceivedAt = new DateTime(2026, 1, 10)
                });

            await context.SaveChangesAsync();

            var exchangeRate = new Mock<IExchangeRateService>();

            exchangeRate
                .Setup(x => x.GetCurrentRateAsync())
                .ReturnsAsync(new ExchangeRateDto
                {
                    ExchangeRate = 6.92m
                });

            var service = new ReportService(
                context,
                exchangeRate.Object,
                new BalanceRequestDtoValidator());

            // Act
            var result = await service.GetBalanceAsync(
                1,
                new BalanceRequestDto
                {
                    StartDate = new DateTime(2026, 1, 1),
                    EndDate = new DateTime(2026, 7, 8),
                    Currency = CurrencyEnum.USD
                });

            // Assert
            Assert.Equal(1200m, result.Total);
        }
    }
}
