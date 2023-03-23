using DataLayer.Repositories;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IVehicleRepository, VehicleRepository>();
        }
    }
}
