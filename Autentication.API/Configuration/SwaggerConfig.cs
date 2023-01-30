using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Autentication.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cancelatin token and Rate Limit", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header usando o padrão Bearer. \r\n\r\n 
                                    Adicione 'Bearer' [espaço] e então seu token no input abaixo.
                                    \r\n\r\nExemplo: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    c.IncludeXmlComments(xmlPath);
                }
            );

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            return app;
        }
    }
}