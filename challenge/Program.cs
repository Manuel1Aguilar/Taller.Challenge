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
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer(connString);
});

// Configure services 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>(); 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
