using AuthService.Infra.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infra.IoC.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var handlers = AppDomain.CurrentDomain.Load("AuthService.Application");
            services.AddMediatR(handlers);

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            //services.AddIdentityCore<IdentityUser>();

            return services;
        }
    }
}
