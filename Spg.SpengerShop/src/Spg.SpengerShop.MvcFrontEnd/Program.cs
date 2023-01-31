using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Repository.Products;
using Spg.SpengerShop.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string? connectionString = builder.Configuration.GetConnectionString("MyConnection");

// Add Services to IServiceCollection
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<IProductRepository, TestProductRepository>();

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