using Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Services;
using System.Reflection;

namespace RealStateApp.Infrastructure.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<ISalesTypeService, SalesTypeService>();
            services.AddTransient<IImprovementsService, ImprovementsService>(); 
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyImprovementsService, PropertyImprovementsService>();
            #endregion
        }
    }
}
