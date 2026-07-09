using Banco_Sol_Gestion_Financiera.Enums;

namespace Banco_Sol_Gestion_Financiera.Entities
{
    public class Income
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public CurrencyEnum Currency { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Source { get; set; } = string.Empty;

        public DateTime ReceivedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}
