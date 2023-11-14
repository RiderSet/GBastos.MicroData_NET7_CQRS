using System;
using GBastos.MicroData.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace GBastos.MicroData.Services.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}