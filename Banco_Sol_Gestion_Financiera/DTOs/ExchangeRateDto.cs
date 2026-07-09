namespace Banco_Sol_Gestion_Financiera.DTOs
{
    public class ExchangeRateDto
    {
        public string BaseCurrency { get; set; } = string.Empty;

        public string TargetCurrency { get; set; } = string.Empty;

        public decimal ExchangeRate { get; set; }

        public int Unit { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
