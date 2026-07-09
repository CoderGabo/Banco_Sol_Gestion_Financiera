using Banco_Sol_Gestion_Financiera.Enums;

namespace Banco_Sol_Gestion_Financiera.DTOs
{
    public class BalanceRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CurrencyEnum Currency { get; set; }
    }
}
