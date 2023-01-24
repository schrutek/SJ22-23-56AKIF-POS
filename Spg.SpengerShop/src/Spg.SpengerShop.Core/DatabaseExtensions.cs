using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.SpengerShop.Infrastructure;
using System.Runtime.CompilerServices;

namespace Spg.SpengerShop.Core
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Meine SQLite-Methode
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void ConfigureSqLite(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SpengerShopContext>(options =>
            {
                if (!options.IsConfigured)
                {
                    options.UseSqlite(connectionString);
                }
            });
        }
    }
}