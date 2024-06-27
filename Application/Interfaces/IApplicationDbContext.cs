using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<RCurrency> RCurrencies { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}