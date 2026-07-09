using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Entities;
using Banco_Sol_Gestion_Financiera.Enums;
using Banco_Sol_Gestion_Financiera.Services.Income;
using Banco_Sol_Gestion_Financiera.Tests.Helpers;
using Banco_Sol_Gestion_Financiera.Validators;

namespace Banco_Sol_Gestion_Financiera.Tests.Services
{
    public class IncomeServiceTest
    {
        [Fact]
        public async Task CreateAsync_ShouldCreateIncome()
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

            await context.SaveChangesAsync();

            var service = new IncomeService(
                context,
                new CreateIncomeDtoValidator()
            );

            var dto = new CreateIncomeDto
            {
                Amount = 1000,
                Currency = "BOB",
                Description = "Salario",
                Source = "Empresa",
                ReceivedAt = DateTime.Today
            };


            // Act
            var result = await service.CreateAsync(dto, 1);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(1000, result.Amount);
            Assert.Equal(CurrencyEnum.BOB, result.Currency);
            Assert.Equal(1, result.UserId);

            Assert.Single(context.Incomes);
        }


        [Fact]
        public async Task CreateAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            // Arrange
            var context = DBContextFactory.Create();

            var service = new IncomeService(
                context,
                new CreateIncomeDtoValidator()
            );

            var dto = new CreateIncomeDto
            {
                Amount = 500,
                Currency = "USD",
                Description = "Extra",
                Source = "Trabajo adicional",
                ReceivedAt = DateTime.Today
            };


            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => service.CreateAsync(dto, 999)
            );
        }


        [Fact]
        public async Task GetMyIncomesAsync_ShouldReturnOnlyUserIncomes()
        {
            // Arrange
            var context = DBContextFactory.Create();

            context.Incomes.AddRange(
                new Income
                {
                    UserId = 1,
                    Amount = 100,
                    Currency = CurrencyEnum.BOB,
                    Description = "Ingreso 1",
                    Source = "Trabajo",
                    ReceivedAt = DateTime.Today
                },
                new Income
                {
                    UserId = 2,
                    Amount = 200,
                    Currency = CurrencyEnum.USD,
                    Description = "Ingreso 2",
                    Source = "Trabajo",
                    ReceivedAt = DateTime.Today
                }
            );

            await context.SaveChangesAsync();

            var service = new IncomeService(
                context,
                new CreateIncomeDtoValidator()
            );


            // Act
            var result = await service.GetMyIncomesAsync(1);


            // Assert
            var incomes = result.ToList();

            Assert.Single(incomes);
            Assert.Equal(100, incomes[0].Amount);
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnIncome_WhenBelongsToUser()
        {
            // Arrange
            var context = DBContextFactory.Create();

            var income = new Income
            {
                UserId = 1,
                Amount = 300,
                Currency = CurrencyEnum.BOB,
                Description = "Ingreso",
                Source = "Trabajo",
                ReceivedAt = DateTime.Today
            };

            context.Incomes.Add(income);

            await context.SaveChangesAsync();

            var service = new IncomeService(
                context,
                new CreateIncomeDtoValidator()
            );


            // Act
            var result = await service.GetByIdAsync(
                income.Id,
                1
            );


            // Assert
            Assert.NotNull(result);
            Assert.Equal(300, result!.Amount);
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenIncomeBelongsToAnotherUser()
        {
            // Arrange
            var context = DBContextFactory.Create();

            var income = new Income
            {
                UserId = 1,
                Amount = 300,
                Currency = CurrencyEnum.BOB,
                Description = "Ingreso",
                Source = "Trabajo",
                ReceivedAt = DateTime.Today
            };

            context.Incomes.Add(income);

            await context.SaveChangesAsync();

            var service = new IncomeService(
                context,
                new CreateIncomeDtoValidator()
            );


            // Act
            var result = await service.GetByIdAsync(
                income.Id,
                2
            );


            // Assert
            Assert.Null(result);
        }
    }
}
