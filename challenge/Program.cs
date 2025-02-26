using challenge.Core.Interfaces.Repositories;
using challenge.Core.Interfaces.Services;
using challenge.Infrastructure;
using challenge.Infrastructure.Repositories;
using challenge.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Database configuration
var connString = builder.Configuration.GetConnectionString("DbConnString");

if (builder.Environment.IsDevelopment())
{
    string debugDbPath = "db/debug.db";
    Console.WriteLine($"Running in Debug Mode: Using SQLite Database at {debugDbPath}");
    builder.Services.AddDbContext<UserContext>(options =>
    {
        options.UseSqlite($"Data Source={debugDbPath}");
    });
}
else
{
    builder.Services.AddDbContext<UserContext>(options =>
    {
        options.UseSqlServer(connString);
    });
}

// Configure services 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations and ensure database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserContext>();

    if (builder.Environment.IsDevelopment())
    {
        Console.WriteLine("Ensuring Debug SQLite Database is Created...");
        db.Database.Migrate(); // Applies migrations, creating the database if needed
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
