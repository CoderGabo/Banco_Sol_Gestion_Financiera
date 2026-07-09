using System.Text.Json.Serialization;

namespace Banco_Sol_Gestion_Financiera.DTOs
{
    public class HexaRateResponseDto
    {
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        public HexaRateDataDto Data { get; set; } = null!;
    }
}
