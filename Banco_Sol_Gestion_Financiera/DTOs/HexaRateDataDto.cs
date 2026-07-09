namespace Banco_Sol_Gestion_Financiera.DTOs
{
    public class HexaRateDataDto
    {
        public string Base { get; set; } = string.Empty;

        public string Target { get; set; } = string.Empty;

        public decimal Mid { get; set; }

        public int Unit { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
