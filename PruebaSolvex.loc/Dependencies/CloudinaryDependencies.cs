using Microsoft.Extensions.DependencyInjection;
using PruebaSolvex.Application.Contract;
using PruebaSolvex.Application.Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.loc.Dependencies
{
    public static class CloudinaryDependencies
    {

        public static void AddCloudinaryDependencies(this IServiceCollection services)
        {

            services.AddScoped<ICloudinaryServices, CloudinaryService>();

        }

    }
}
