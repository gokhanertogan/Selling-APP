using Microsoft.EntityFrameworkCore;
using Payment.API.EntityConfiguration;

namespace Payment.API.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Entities.Payment> Payments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());

        modelBuilder.Entity<Entities.Payment>().HasData(
            new Entities.Payment { Id = 1, PaymentType = "Cash", Amount = 100.50m },
            new Entities.Payment { Id = 2, PaymentType = "Credit Card", Amount = 200.75m },
            new Entities.Payment { Id = 3, PaymentType = "Bank Transfer", Amount = 300.25m }
        );
    }
}
