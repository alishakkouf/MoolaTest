using BackendTask.Domain.Contract.TransactionsContract;
using BackendTask.Data.Providers;
using BackendTask.Manager;
using BackendTask.WinService;
using BackendTask.Data;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using BackendTask.Common;
using BackendTask.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

const string ConnectionStringName = "DefaultConnection";

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

builder.Services.AddHostedService<Worker>();

// Add this line before builder.Build()
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Program.cs
builder.Services.AddSingleton<IHttpContextAccessor>(new FakeHttpContextAccessor());

builder.Services.AddDbContext<BackendTaskDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString(ConnectionStringName)));

builder.Services.AddScoped<ITransactionsManager, TransactionsManager>();
builder.Services.AddScoped<ITransactoinProvider, TransactoinProvider>();

var host = builder.Build();
host.Run();