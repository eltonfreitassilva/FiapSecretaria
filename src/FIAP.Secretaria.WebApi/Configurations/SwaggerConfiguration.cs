using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace FIAP.Secretaria.WebApi.Configurations;

public static class SwaggerConfiguration
{
    private static string API_NAME = "FIAP API.Secretaria ";
    private static string API_DESCRIPTION = "API Secretaria FIAP - v1.0";

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IWebHostEnvironment environment)
    {
        ConfigureNames(environment);

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = API_NAME,
                Description = API_DESCRIPTION
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    Array.Empty<string>()
                }
            });

            var basicSecurityScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                Reference = new OpenApiReference
                {
                    Id = "BasicAuth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    basicSecurityScheme,
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerCustomUI(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("v1/swagger.json", API_NAME);
            opt.DocumentTitle = API_NAME;
            opt.RoutePrefix = "swagger";
        });

        return app;
    }

    private static void ConfigureNames(IWebHostEnvironment environment)
    {
        API_NAME += " (Desenvolvimento)";
        API_DESCRIPTION += " (Desenvolvimento)";

        if (environment.IsProduction())
        {
            API_NAME += " (Produção)";
            API_DESCRIPTION += " (Produção)";
        }

        if (environment.IsStaging())
        {
            API_NAME += " (Homologação)";
            API_DESCRIPTION += " (Homologação)";
        }
    }
}
