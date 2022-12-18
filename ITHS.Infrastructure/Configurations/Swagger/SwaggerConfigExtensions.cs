using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ITHS.Infrastructure.Configurations.Swagger;

/// <summary>
/// Swagger example setup using versioning and jwt authentication schema
/// </summary>
public static class SwaggerConfigExtensions {
    const string Bearer = "Bearer";

    /// <summary>
    /// Custom swagger setup with versioning and JWT bearer authentication
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection service) {
        service.AddEndpointsApiExplorer();

        service.AddApiVersioning((setup) => {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });
    
        service.AddSwaggerGen((options) => {
            options.SwaggerDoc("v1", V1);
            options.SwaggerDoc("v2", V2);
            options.SwaggerDoc("v3", V3);

            options.AddSecurityDefinition(Bearer, JwtBearerSecurityScheme);

            options.AddSecurityRequirement(SecurityRequirement);

            options.IncludeXmlComments(PathToXMLComments);
        });
    
        return service;
    }

    /// <summary>
    /// Use custom swagger and Custom swagger UI
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseSwaggerConfigurations(this WebApplication app) {
        app.UseSwagger();

        app.UseSwaggerUI((options) => {
            options.RoutePrefix = "swagger";
            string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
            options.SwaggerEndpoint("v1/swagger.json", "Demo doc V1");
            options.SwaggerEndpoint("v2/swagger.json", "Demo doc V2");
            options.SwaggerEndpoint("v3/swagger.json", "Demo doc V3");
        });

        return app;
    }

    static string PathToXMLComments => Path.Combine(AppContext.BaseDirectory, XmlFileNameFromExecutedAssembly);

    static string XmlFileNameFromExecutedAssembly => $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";

    static OpenApiSecurityScheme JwtBearerSecurityScheme {
        get => new OpenApiSecurityScheme {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Please enter a valid token",
            Name = "Authorization",
            BearerFormat = "JWT",
            Scheme = Bearer
        };
    }

    static OpenApiInfo V1 => new OpenApiInfo {
        Title = "ITHS - V1",
        Version = "v1",
        Description = "Here we go v1 of the api",
        TermsOfService = new Uri("http://toSomewhere.com"),
        Contact = ContactIInformation,
        License = LicenseDescription
    };

    static OpenApiInfo V2 => new OpenApiInfo {
        Title = "ITHS - V2",
        Version = "v2",
        Description = "Here we go v2 of the api",
        TermsOfService = new Uri("http://toSomewhere.com"),
        Contact = ContactIInformation,
        License = LicenseDescription
    };

    static OpenApiInfo V3 => new OpenApiInfo {
        Title = "ITHS - V3",
        Version = "v3",
        Description = "Here we go v3 of the api",
        TermsOfService = new Uri("http://toSomewhere.com"),
        Contact = ContactIInformation,
        License = LicenseDescription
    };

    static OpenApiLicense LicenseDescription => new OpenApiLicense {
        Name = "Apache 2.0",
        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
    };

    static OpenApiContact ContactIInformation => new OpenApiContact {
        Name = "Author",
        Email = "author@example.com"
    };

    static OpenApiSecurityRequirement SecurityRequirement => new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type=ReferenceType.SecurityScheme,
                    Id=Bearer
                }
            },
            Array.Empty<string>()
        }
    };
}
