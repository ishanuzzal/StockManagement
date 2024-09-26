using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Mappers;
using Service.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static void RegisterServiceLayerDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IAuthService,AuthService>();

        }
    }
}
