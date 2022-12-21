using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ITHS.Infrastructure.Configurations.Swagger;

/// <summary>
/// Swagger example setup using versioning and JWT authentication schema
/// </summary>
public static class SwaggerConfigExtensions {
    const string Bearer = "Bearer";

    /// <summary>
    /// Custom Swagger setup with versioning and JWT bearer authentication
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

            options.AddSecurityDefinition(Bearer, JwtBearerSecurityScheme);

            options.AddSecurityRequirement(SecurityRequirement);

            options.IncludeXmlComments(PathToXMLComments);
        });
    
        return service;
    }

    /// <summary>
    /// Use custom Swagger and custom Swagger UI
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseSwaggerConfigurations(this WebApplication app) {
        app.UseSwagger();

        app.UseSwaggerUI((options) => {
            options.RoutePrefix = "swagger";
            string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
            options.SwaggerEndpoint("v1/swagger.json", "Quiz v1");
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
        Title = "Quiz",
        Version = "v1",
        Description = "Here we go - v1 of the API",
        TermsOfService = new Uri("https://www.example.com"),
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
