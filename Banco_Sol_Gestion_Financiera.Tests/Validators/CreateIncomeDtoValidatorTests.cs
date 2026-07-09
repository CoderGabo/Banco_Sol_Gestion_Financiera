using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_Sol_Gestion_Financiera.Tests.Validators
{
    public class CreateIncomeDtoValidatorTests
    {
        private readonly CreateIncomeDtoValidator _validator = new();

        [Fact]
        public void ShouldAcceptValidIncome()
        {
            var dto = new CreateIncomeDto
            {
                Amount = 100,
                Currency = "BOB",
                Description = "Salario",
                Source = "Empresa",
                ReceivedAt = DateTime.Today
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldRejectInvalidCurrency()
        {
            var dto = new CreateIncomeDto
            {
                Amount = 100,
                Currency = "EUR",
                Description = "Ingreso",
                Source = "Trabajo",
                ReceivedAt = DateTime.Today
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldRejectAmountLessThanOrEqualZero()
        {
            var dto = new CreateIncomeDto
            {
                Amount = 0,
                Currency = "USD",
                Description = "Ingreso",
                Source = "Trabajo",
                ReceivedAt = DateTime.Today
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldRejectEmptyDescription()
        {
            var dto = new CreateIncomeDto
            {
                Amount = 100,
                Currency = "USD",
                Description = "",
                Source = "Trabajo",
                ReceivedAt = DateTime.Today
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldRejectEmptySource()
        {
            var dto = new CreateIncomeDto
            {
                Amount = 100,
                Currency = "USD",
                Description = "Ingreso",
                Source = "",
                ReceivedAt = DateTime.Today
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldRejectFutureReceivedDate()
        {
            var dto = new CreateIncomeDto
            {
                Amount = 100,
                Currency = "USD",
                Description = "Ingreso",
                Source = "Trabajo",
                ReceivedAt = DateTime.Today.AddDays(1)
            };

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
        }
    }
}
