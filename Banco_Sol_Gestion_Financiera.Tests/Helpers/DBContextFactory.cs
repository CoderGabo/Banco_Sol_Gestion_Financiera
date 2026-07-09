using Banco_Sol_Gestion_Financiera.Data;
using Microsoft.EntityFrameworkCore;

namespace Banco_Sol_Gestion_Financiera.Tests.Helpers
{
    public class DBContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }
    }
}
