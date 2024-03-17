using Microsoft.EntityFrameworkCore;
using Order.API.EntityConfiguration;

namespace Order.API.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Entities.Order> Orders { get; set; }
    public DbSet<Entities.PaymentHistory> PaymentHistories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentHistoryConfiguration());
    }
}