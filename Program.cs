using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 2, 0));
builder.Services.AddDbContext<FisierDb>(opt => opt.UseMySql(connString, serverVersion));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:7147",
                                "http://localhost:5173");
        });
});

var app = builder.Build();

app.UseCors();

app.MapControllers();
app.Run();
