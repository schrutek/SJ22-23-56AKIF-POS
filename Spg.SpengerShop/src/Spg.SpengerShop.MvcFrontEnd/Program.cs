using MediatR;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.CQRS.Products.Queries;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Core;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Products;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string? connectionString = builder.Configuration.GetConnectionString("MyConnection");

// Add Services to IServiceCollection
builder.Services.AddTransient<IReadOnlyProductService, ProductService>();
builder.Services.AddTransient<IAddableProductService, ProductService>();
builder.Services.AddTransient<IDateTimeService, DateTimeService>();

// CRUD Services
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IReadOnlyProductRepository, ProductRepository>();
builder.Services.AddTransient<IReadOnlyRepositoryBase<Product>, RepositoryBase<Product>>();
builder.Services.AddTransient<IReadOnlyRepositoryBase<Category>, RepositoryBase<Category>>();

// MediatR
builder.Services.AddMediatR(config => 
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient<IRequestHandler<GetProductByNameRequest, Product>, GetProductByNameRequestHandler>();

builder.Services.ConfigureSqLite(connectionString);

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

app.UseAuthorization();

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