using Microsoft.Extensions.DependencyInjection;
using PruebaSolvex.Application.Contract;
using PruebaSolvex.Application.Servicies;
using PruebaSolvex.Persistence.Interfaces;
using PruebaSolvex.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.loc.Dependencies
{
    public static class ProductDependencies
    {

        public static void AddProductServices(this IServiceCollection services)
        {
            services.AddScoped<IProduct, ProductRepository>();
            services.AddTransient<IProductServices, ProductServices>();
        }

    }
}
