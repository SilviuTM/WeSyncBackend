using Microsoft.EntityFrameworkCore;

public class FisierDb : DbContext
{
    public FisierDb(DbContextOptions<FisierDb> options)
        : base(options) { }

    public DbSet<Fisier> Fisiers => Set<Fisier>();
}