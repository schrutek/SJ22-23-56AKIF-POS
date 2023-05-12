using MediatR;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.Account;
using Spg.SpengerShop.Application.CQRS.Products.FilterByExpiryDate.Queries;
using Spg.SpengerShop.Application.CQRS.Products.GetByName.Queries;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Core;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.MvcFrontEnd.Filters;
using Spg.SpengerShop.MvcFrontEnd.Helpers;
using Spg.SpengerShop.MvcFrontEnd.Services;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Products;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews(configure => configure.Filters.Add(new AuthorisationFilterAttribute()));

string? connectionString = builder.Configuration.GetConnectionString("MyConnection");

// Http-Service (Cookies)
builder.Services.AddTransient<HttpService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add Services to IServiceCollection
builder.Services.AddTransient<IReadOnlyProductService, ProductService>();
builder.Services.AddTransient<IAddableProductService, ProductService>();
builder.Services.AddTransient<IDateTimeService, DateTimeService>();

// CRUD Services
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IReadOnlyProductRepository, ProductRepository>();
builder.Services.AddTransient<IReadOnlyRepositoryBase<Product>, RepositoryBase<Product>>();
builder.Services.AddTransient<IReadOnlyRepositoryBase<Category>, RepositoryBase<Category>>();

builder.Services.AddTransient<IAuthService, LocalDbLoginService>();

// MediatR
builder.Services.AddMediatR(config => 
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient<IRequestHandler<GetProductByNameRequest, Product>, GetProductByNameRequestHandler>();
builder.Services.AddTransient<IRequestHandler<GetByExpiryDateRequest, IQueryable<Product>>, GetByExpiryDateRequestHandler>();

builder.Services.ConfigureSqLite(connectionString);

//builder.Services.AddControllers(config =>
//{
//    config.Filters.Create(new ValidationFilterAttribute());
//});

// Authentication ********************************************************************
// Soll ein gespeichertes Secret verwendet werden, kann folgende Zeite statt dessen
// verwendet werden:
string jwtSecret = builder.Configuration["AppSettings:Secret"] ?? HashHelper.GenerateRandom(1024);

// JWT aktivieren, aber nicht standardmäßig aktivieren. Daher muss beim Controller
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// geschrieben werden. Wird nur eine API bereitgestellt, kann dieser Parameter auf
// true gesetzt und Cookies natürlich deaktiviert werden.
builder.Services.AddJwtAuthentication(jwtSecret, setDefault: true);

// Cookies aktivieren. Dies ist für Blazor oder MVC Applikationen gedacht.
builder.Services.AddCookieAuthentication(setDefault: false);

// Instanzieren des Userservices mit einer Factorymethode. Diese übergibt das gespeicherte
// Secret.
//builder.Services.AddTransient<ApiAuthService>();
// oder folgende zeile mit einem Secret aus der appsettings.json
builder.Services.AddScoped<MvcApiAuthService>(services =>
    new MvcApiAuthService(jwtSecret, new LocalDbLoginService()));

builder.Services.AddHttpContextAccessor();
// Authentication ********************************************************************


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public class Xy
{
    public void DoSomething()
    {
        
    }
}