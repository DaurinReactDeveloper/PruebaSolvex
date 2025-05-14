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
    public static class UserDependencies
    {

        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUser, UserRepository>();
            services.AddTransient<IUserServices, UserServices>();
        }

    }
}
