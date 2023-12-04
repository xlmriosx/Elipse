using Elipse.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////
string server = Environment.GetEnvironmentVariable("DB_SERVER");
string database = Environment.GetEnvironmentVariable("DB_DATABASE");
string user = Environment.GetEnvironmentVariable("DB_USER");
string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

string connectionString = $"Server={server};Database={database};User Id={user};Password={password};";

// Use the connection string in your application, e.g., configure your database context.
services.AddDbContext<ChatContext>(options =>
    options.UseSqlServer(connectionString));

////

// builder.Services.AddDbContext<ChatContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("ChatContext") ?? throw new InvalidOperationException("Connection string 'ChatContext' not found.")));

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

