namespace Infrastructure.Persistence;

public class DbInitialize
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
    }
}