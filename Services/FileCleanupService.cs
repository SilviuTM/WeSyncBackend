using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class FileCleanupService : BackgroundService
{
    private readonly IServiceProvider services;

    public FileCleanupService(IServiceProvider services)
    {
        this.services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Run the cleanup logic at 12 PM daily
            DateTime nextRunTime = DateTime.Today.AddDays(1).AddHours(12);

            // Calculate the time until the next run
            TimeSpan delay = nextRunTime - DateTime.Now;

            // Delay until the next run
            await Task.Delay(delay, stoppingToken);

            // Perform cleanup
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                CleanupExpiredFiles(dbContext);
            }
        }
    }

    private void CleanupExpiredFiles(AppDbContext dbContext)
    {
        // Query for files with expiration time passed
        var expiredFiles = dbContext.Fisiers
        .Where(f => f.ExpirationTime != null && f.ExpirationTime < DateTime.UtcNow)
        .ToList();

        // Remove the expired files from the DbSet
        dbContext.Fisiers.RemoveRange(expiredFiles);

        dbContext.SaveChanges();
    }
}