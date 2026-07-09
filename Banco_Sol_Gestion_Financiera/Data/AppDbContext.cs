using Banco_Sol_Gestion_Financiera.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banco_Sol_Gestion_Financiera.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();

        public DbSet<Income> Incomes => Set<Income>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("Incomes");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Currency)
                  .HasConversion<string>()
                  .HasMaxLength(3);

                entity.Property(e => e.Description)
                    .HasMaxLength(255);

                entity.Property(e => e.Source)
                    .HasMaxLength(50);

                entity.Property(e => e.ReceivedAt)
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone");

                entity.HasOne(i => i.User)
                  .WithMany(u => u.Incomes)
                  .HasForeignKey(i => i.UserId);
            });
        }
    }
}
