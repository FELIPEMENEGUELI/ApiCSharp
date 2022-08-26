using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.ApiDotNet6.Application.Mappings;
using MP.ApiDotNet6.Application.Services;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Authentication;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Authentication;
using MP.ApiDotNet6.Infra.Data.Context;
using MP.ApiDotNet6.Infra.Data.Repositories;

namespace MP.ApiDotNet6.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            service.AddScoped<IPersonRepository, PersonRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IPurchaseRepository, PurchaseRepository>();
            service.AddScoped<IUnitOffWork, UnitOffWork>();
            service.AddScoped<ITokenGenerator, TokenGenerator>();
            service.AddScoped<IUserRepository, UserRepository>();
            return service;
           
        }

        public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAutoMapper(typeof(DomainToDtoMapping));
            service.AddScoped<IPersonServices, PersonServices>();
            service.AddScoped<IProductServices, ProductServices>();
            service.AddScoped<IPurchaseServices, PurchaseServices>();
            service.AddScoped<IUserServices, UserServices>();
            return service;
   
        }
    }
}
