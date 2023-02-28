﻿// Service aufrufen:

using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.Products;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;

DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlite("Data Source=./../../../SpengerShop.db");

SpengerShopContext db = new SpengerShopContext(optionsBuilder.Options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
db.Seed();

DbContextOptionsBuilder optionsBuilder2 = new DbContextOptionsBuilder();
optionsBuilder2.UseSqlite("Data Source=./../../../SpengerShop_Test.db");

SpengerShopContext db2 = new SpengerShopContext(optionsBuilder.Options);
db2.Database.EnsureDeleted();
db2.Database.EnsureCreated();
db2.Seed(); 

IQueryable<Product> result = new ProductService(new RepositoryBase<Product>(db), new RepositoryBase<Product>(db)).GetAll();
foreach (Product p in result.ToList())
{
    Console.WriteLine($"{p.Name} - {p.Ean} - {p.Material}");
}