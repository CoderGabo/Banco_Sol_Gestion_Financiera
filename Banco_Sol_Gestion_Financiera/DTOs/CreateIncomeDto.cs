using Banco_Sol_Gestion_Financiera.Enums;

namespace Banco_Sol_Gestion_Financiera.DTOs
{
    public class CreateIncomeDto
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Source { get; set; } = string.Empty;

        public DateTime ReceivedAt { get; set; }
    }
}
