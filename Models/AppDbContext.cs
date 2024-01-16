using Microsoft.EntityFrameworkCore;
using WeSyncBackend.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Fisier> Fisiers => Set<Fisier>();
    public DbSet<User> Users => Set<User>();
}