using Banco_Sol_Gestion_Financiera.Services.ExchangeRate;
using System.Net;
using System.Text;

namespace Banco_Sol_Gestion_Financiera.Tests.Services
{
    public class ExchangeRateServiceTests
    {
        [Fact]
        public async Task ShouldReturnExchangeRateSuccessfully()
        {
            var json = """
            {
              "data": {
                "base": "USD",
                "target": "BOB",
                "mid": 9.925,
                "unit": 1,
                "timestamp": "2026-07-09T00:07:48.934Z"
              }
            }
            """;


            var handler = new FakeHttpMessageHandler(
                json,
                HttpStatusCode.OK
            );


            var client = new HttpClient(handler)
            {
                BaseAddress =
                    new Uri("https://hexarate.paikama.co/api/")
            };


            var service =
                new ExchangeRateService(client);


            var result =
                await service.GetCurrentRateAsync();


            Assert.NotNull(result);

            Assert.Equal(
                "USD",
                result.BaseCurrency
            );

            Assert.Equal(
                "BOB",
                result.TargetCurrency
            );

            Assert.Equal(
                9.925m,
                result.ExchangeRate
            );
        }



        [Fact]
        public async Task ShouldThrowWhenApiFails()
        {
            var handler = new FakeHttpMessageHandler(
                "",
                HttpStatusCode.BadRequest
            );


            var client = new HttpClient(handler)
            {
                BaseAddress =
                    new Uri("https://hexarate.paikama.co/api/")
            };


            var service =
                new ExchangeRateService(client);


            await Assert.ThrowsAsync<HttpRequestException>(
                () => service.GetCurrentRateAsync()
            );
        }



        [Fact]
        public async Task ShouldThrowWhenResponseHasNoData()
        {
            var json = """
            {
              "data": null
            }
            """;


            var handler = new FakeHttpMessageHandler(
                json,
                HttpStatusCode.OK
            );


            var client = new HttpClient(handler)
            {
                BaseAddress =
                    new Uri("https://hexarate.paikama.co/api/")
            };


            var service =
                new ExchangeRateService(client);


            await Assert.ThrowsAsync<Exception>(
                () => service.GetCurrentRateAsync()
            );
        }



        private class FakeHttpMessageHandler : HttpMessageHandler
        {
            private readonly string _response;
            private readonly HttpStatusCode _statusCode;


            public FakeHttpMessageHandler(
                string response,
                HttpStatusCode statusCode)
            {
                _response = response;
                _statusCode = statusCode;
            }


            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {

                return Task.FromResult(
                    new HttpResponseMessage(_statusCode)
                    {
                        Content =
                            new StringContent(
                                _response,
                                Encoding.UTF8,
                                "application/json")
                    });
            }
        }
    }
}
