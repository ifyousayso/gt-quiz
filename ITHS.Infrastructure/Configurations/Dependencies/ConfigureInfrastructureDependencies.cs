using ITHS.Domain.Interfaces.Repositories;
using ITHS.Infrastructure.Contexts.NobelPrize.Repositories;
using ITHS.Infrastructure.Contexts.Persons.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITHS.Infrastructure.Configurations.Dependencies;

public static class ConfigureInfrastructureDependencies {
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration config) {
        //TODO: Implement infrastructure dependencies here instead of bloating the Program.cs file
        return services
            .AddScoped<INobelPrizeRepository, NobelPrizeRepository>()
            .AddScoped<IPersonsRepository, PersonsRepository>();
    }
}
