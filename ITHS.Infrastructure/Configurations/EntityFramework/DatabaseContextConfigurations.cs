using ITHS.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITHS.Infrastructure.Configurations.EntityFramework;

public static class DatabaseContextConfigurations {
    /// <summary>
    /// Creates the database file sqlite.db if it doesn't exist
    /// </summary>
    /// <param name="services"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static IServiceCollection AddITHSDbContextUsingSqlite(this IServiceCollection services, string fileName = "sqlite.db") {
        return services
            .AddDbContext<ITHSDatabaseContext>((options) =>
                options.UseSqlite($"Data Source={fileName}"),
                ServiceLifetime.Scoped
        );
    }

    /// <summary>
    /// Configure the app for SQL server
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddITHSDbContextUsingSqlServer(this IServiceCollection services, IConfiguration config) {
        string ConnectionString = GetConnectionString(config, "ITHSDatabase");

        return services
            .AddDbContext<ITHSDatabaseContext>((options) =>
                options.UseSqlServer(ConnectionString),
                ServiceLifetime.Scoped
        );
    }

    /// <summary>
    /// Retrieve the connection string
    /// </summary>
    private static string GetConnectionString(IConfiguration config, string name) {
        var ConnectionString = config.GetConnectionString(name);
    
        if (ConnectionString == null) {
            throw new Exception("No connection string found in appsettings.json");
        }

        return ConnectionString;
    }
}
