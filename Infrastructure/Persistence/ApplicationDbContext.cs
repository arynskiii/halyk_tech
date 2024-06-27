using Application.Interfaces;
using Domain;
using Infrastructure.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<RCurrency> RCurrencies { get; set; }
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> dbContextOptions) 
        : base(dbContextOptions) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RCurrencyConfiguration());
        base.OnModelCreating(builder);
    }
    
}