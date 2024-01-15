using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });


string startupPath = Directory.GetCurrentDirectory();
string DbPath = Path.Join(startupPath, "Database", "MyDatabase.sqlite");
if (!File.Exists(DbPath))
{
    SQLiteConnection.CreateFile(DbPath);
}

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
//var serverVersion = new MySqlServerVersion(new Version(8, 2, 0));
builder.Services.AddDbContext<FisierDb>(opt => opt.UseSqlite($"Data Source={DbPath}"));
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();



app.MapControllers();
app.Run();
