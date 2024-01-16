using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });
builder.Services.AddHostedService<FileCleanupService>();


string startupPath = Directory.GetCurrentDirectory();
string DbPath = Path.Join(startupPath, "Database", "MyDatabase.sqlite");
if (!File.Exists(DbPath))
{
    SQLiteConnection.CreateFile(DbPath);
}

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite($"Data Source={DbPath}"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();




var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.MapControllers();
app.Run();
