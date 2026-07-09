using Banco_Sol_Gestion_Financiera.Data;
using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Entities;
using Banco_Sol_Gestion_Financiera.Services.User;
using Banco_Sol_Gestion_Financiera.Tests.Helpers;
using Banco_Sol_Gestion_Financiera.Validators;

namespace Banco_Sol_Gestion_Financiera.Tests.Services
{
    public class UserServiceTests
    {
        private UserService CreateService(AppDbContext context)
        {
            return new UserService(
                context,
                new CreateUserDtoValidator()
            );
        }


        [Fact]
        public async Task ShouldCreateUserSuccessfully()
        {
            var context = DBContextFactory.Create();

            var service = CreateService(context);


            var dto = new CreateUserDto
            {
                Name = "Gabriel",
                Email = "gabriel@test.com",
                Password = "12345678"
            };


            var result = await service.CreateAsync(dto);


            Assert.NotNull(result);
            Assert.Equal("Gabriel", result.Name);
            Assert.Equal("gabriel@test.com", result.Email);


            var userDb = context.Users.First();

            Assert.NotEqual(
                "12345678",
                userDb.Password
            );
        }



        [Fact]
        public async Task ShouldRejectDuplicatedEmail()
        {
            var context = DBContextFactory.Create();


            context.Users.Add(
                new User
                {
                    Name = "Usuario existente",
                    Email = "test@test.com",
                    Password = "hash"
                }
            );


            await context.SaveChangesAsync();


            var service = CreateService(context);


            var dto = new CreateUserDto
            {
                Name = "Otro usuario",
                Email = "test@test.com",
                Password = "12345678"
            };


            await Assert.ThrowsAsync<ArgumentException>(
                () => service.CreateAsync(dto)
            );
        }



        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var context = DBContextFactory.Create();


            context.Users.AddRange(
                new User
                {
                    Name = "User 1",
                    Email = "user1@test.com",
                    Password = "123"
                },
                new User
                {
                    Name = "User 2",
                    Email = "user2@test.com",
                    Password = "123"
                }
            );


            await context.SaveChangesAsync();


            var service = CreateService(context);


            var result =
                await service.GetAllAsync();


            Assert.Equal(
                2,
                result.Count()
            );
        }



        [Fact]
        public async Task ShouldGetUserById()
        {
            var context = DBContextFactory.Create();


            var user = new User
            {
                Name = "Gabriel",
                Email = "gabriel@test.com",
                Password = "123"
            };


            context.Users.Add(user);

            await context.SaveChangesAsync();


            var service = CreateService(context);


            var result =
                await service.GetByIdAsync(user.Id);


            Assert.NotNull(result);
            Assert.Equal(
                "Gabriel",
                result!.Name
            );
        }



        [Fact]
        public async Task ShouldReturnNullWhenUserDoesNotExist()
        {
            var context = DBContextFactory.Create();

            var service = CreateService(context);


            var result =
                await service.GetByIdAsync(999);


            Assert.Null(result);
        }



        [Fact]
        public async Task ShouldRejectInvalidUserData()
        {
            var context = DBContextFactory.Create();

            var service = CreateService(context);


            var dto = new CreateUserDto
            {
                Name = "",
                Email = "correo-invalido",
                Password = "123"
            };


            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                () => service.CreateAsync(dto)
            );
        }
    }
}
