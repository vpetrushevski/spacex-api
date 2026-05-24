using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.WebApi.Extensions;

public static class WebApiServiceCollectionExtensions
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
            });
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Space X Web API",
                Version = "v1",
                Description = "FSpace X Web API documentation"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter: Bearer {token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddControllers(options =>
        {
            options.Filters.Add(
                new ProducesResponseTypeAttribute(
                    typeof(ApiResponse<string>),
                    StatusCodes.Status400BadRequest));

            options.Filters.Add(
                new ProducesResponseTypeAttribute(
                    typeof(ApiResponse<string>),
                    StatusCodes.Status401Unauthorized));

            options.Filters.Add(
                new ProducesResponseTypeAttribute(
                    typeof(ApiResponse<string>),
                    StatusCodes.Status403Forbidden));

            options.Filters.Add(
                new ProducesResponseTypeAttribute(
                    typeof(ApiResponse<string>),
                    StatusCodes.Status404NotFound));

            options.Filters.Add(
                new ProducesResponseTypeAttribute(
                    typeof(ApiResponse<string>),
                    StatusCodes.Status500InternalServerError));
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
                ControllerExtensions.ValidationErrorResponse(actionContext.ModelState);
        });

        return services;
    }
}

