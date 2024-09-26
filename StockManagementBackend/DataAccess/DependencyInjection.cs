using DataAccess.Repositories.IRepositories;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataBaseContext;
using DataAccess.Interceptors;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using DataAccess.unitOfWork;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static void RegisterDataAccessLayerDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("dbcs"));
                options.AddInterceptors(new TimeAuditableInterceptor());
            });

            services.AddIdentity<Users, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

        }
    }
}
