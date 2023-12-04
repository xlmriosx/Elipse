using Elipse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

builder.Services.AddDbContext<ChatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString) ?? throw new InvalidOperationException("Connection string 'ChatContext' not found.")));

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

