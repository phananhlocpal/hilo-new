using Microsoft.EntityFrameworkCore;
using SaleService.Models;
using SaleService.Repositories.FoodRepository;
using SaleService.Repositories.InvoiceFoodRepository;
using SaleService.Repositories.InvoiceRepository;
<<<<<<< HEAD
using SaleService.Service.RabbitMQServices;
using SaleService.Services.VNPayService;
using Microsoft.Extensions.Caching.StackExchangeRedis;
=======
>>>>>>> 960a83c (commit)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<< HEAD
builder.Services.AddControllers();

// Register distributed Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "SaleServiceSession";
});

// Configure session to use Redis
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "SaleServiceSession";
    // Optionally set other session options here
});

// Register DbContext
=======

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

>>>>>>> 960a83c (commit)
builder.Services.AddDbContext<SaleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpClient
builder.Services.AddHttpClient("ScheduleService", client =>
{
<<<<<<< HEAD
    client.BaseAddress = new Uri("https://localhost:5003/api/Schedule/");
});

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories
=======
    client.BaseAddress = new Uri("https://localhost:5003/api/Schedule/"); // Ensure this URL is correct
});

builder.Services.AddHttpClient("EmployeeService", client =>
{
    client.BaseAddress = new Uri("https://localhost:5006/api/Employee/"); // Ensure this URL is correct
});

builder.Services.AddHttpClient("CustomerService", client =>
{
    client.BaseAddress = new Uri("https://localhost:5005/api/Customer/"); // Ensure this URL is correct
});

// Register AutoMap
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

>>>>>>> 960a83c (commit)
builder.Services.AddScoped<IFoodRepo, FoodRepo>();
builder.Services.AddScoped<IInvoiceFoodRepo, InvoiceFoodRepo>();
builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();

<<<<<<< HEAD
// Register services
builder.Services.AddSingleton<IVnPayService, VnPayService>();
builder.Services.AddScoped<SalePublisherService>();

// Register HttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

// Configure CORS
=======
>>>>>>> 960a83c (commit)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:2000")
               .AllowAnyHeader()
               .AllowAnyMethod()
<<<<<<< HEAD
               .AllowCredentials();
=======
                .AllowCredentials();
>>>>>>> 960a83c (commit)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
app.UseHttpsRedirection();
app.UseCors();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
=======
app.UseCors();


app.UseHttpsRedirection();


app.UseAuthorization();

>>>>>>> 960a83c (commit)
app.MapControllers();

app.Run();
