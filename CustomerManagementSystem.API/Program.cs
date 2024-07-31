using AutoMapper;
using CustomerManagementSystem.Infrastructure.Database;
using CustomerManagementSystem.Infrastructure.Profiles;
using CustomerManagementSystem.Infrastructure.Repositories;
using CustomerManagementSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<WebAppContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new CustomerProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7234")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType)
        .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DbInitializer.EnsureSeedData(app);

app.Run();
