using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Entities;
using Banco_Sol_Gestion_Financiera.Services.Auth;
using Banco_Sol_Gestion_Financiera.Tests.Helpers;
using Moq;

namespace Banco_Sol_Gestion_Financiera.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task ShouldLoginSuccessfully()
        {
            var context = DBContextFactory.Create();


            var user = new User
            {
                Id = 1,
                Name = "Gabriel",
                Email = "gabriel@test.com",
                Password =
                    BCrypt.Net.BCrypt.HashPassword("123456")
            };


            context.Users.Add(user);

            await context.SaveChangesAsync();


            var jwtMock = new Mock<IJwtService>();

            jwtMock
                .Setup(x =>
                    x.GenerateToken(
                        user.Id,
                        user.Email,
                        user.Name))
                .Returns("fake-token");


            var service =
                new AuthService(
                    context,
                    jwtMock.Object
                );


            var result =
                await service.LoginAsync(
                    new LoginDto
                    {
                        Email = "gabriel@test.com",
                        Password = "123456"
                    });


            Assert.NotNull(result);

            Assert.Equal(
                "fake-token",
                result!.Token
            );


            jwtMock.Verify(
                x =>
                    x.GenerateToken(
                        user.Id,
                        user.Email,
                        user.Name),
                Times.Once
            );
        }



        [Fact]
        public async Task ShouldReturnNullWhenUserDoesNotExist()
        {
            var context =
                DBContextFactory.Create();


            var jwtMock =
                new Mock<IJwtService>();


            var service =
                new AuthService(
                    context,
                    jwtMock.Object
                );


            var result =
                await service.LoginAsync(
                    new LoginDto
                    {
                        Email = "noexiste@test.com",
                        Password = "123456"
                    });


            Assert.Null(result);


            jwtMock.Verify(
                x =>
                    x.GenerateToken(
                        It.IsAny<int>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                Times.Never
            );
        }



        [Fact]
        public async Task ShouldReturnNullWhenPasswordIsInvalid()
        {
            var context =
                DBContextFactory.Create();


            context.Users.Add(
                new User
                {
                    Id = 1,
                    Name = "Gabriel",
                    Email = "gabriel@test.com",
                    Password =
                        BCrypt.Net.BCrypt.HashPassword("correcta")
                }
            );


            await context.SaveChangesAsync();


            var jwtMock =
                new Mock<IJwtService>();


            var service =
                new AuthService(
                    context,
                    jwtMock.Object
                );


            var result =
                await service.LoginAsync(
                    new LoginDto
                    {
                        Email = "gabriel@test.com",
                        Password = "incorrecta"
                    });


            Assert.Null(result);


            jwtMock.Verify(
                x =>
                    x.GenerateToken(
                        It.IsAny<int>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                Times.Never
            );
        }
    }
}
