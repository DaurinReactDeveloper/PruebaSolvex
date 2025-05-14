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
    public static class PasswordHelperDependencies
    {

        public static void AddPasswordHelperDependencies(this IServiceCollection services)
        {

            services.AddScoped<IPasswordHelperServices, PasswordHelperService>();

        }

    }
}
