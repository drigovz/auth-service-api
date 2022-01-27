using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Infra.Data.Context;
using AuthService.Infra.Data.Repository;
using AuthService.Infra.Data.Repository.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infra.IoC.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
            );

            services.AddScoped(typeof(IBaseRepository<BaseEntity, Guid>), typeof(BaseRepository<BaseEntity, Guid>));
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}