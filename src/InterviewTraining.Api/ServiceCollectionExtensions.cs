using InterviewTraining.Application;
using InterviewTraining.Infrastructure;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace InterviewTraining.Api;

/// <summary>
/// Startup
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    ///<param name="services"></param>
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors();
        services.AddHealthChecks();

        // Добавляем Memory Cache
        services.AddMemoryCache();

        var identityServerAuthenticationAuthority = ConfigHelper.GetSettingFromConfig(configuration, "IdentityServerAuthentication", "Authority");
        var identityServerAuthenticationApiName = ConfigHelper.GetSettingFromConfig(configuration, "IdentityServerAuthentication", "ApiName");
        // Маппинг claim-типов для корректной работы ролей
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
                options.Authority = identityServerAuthenticationAuthority;
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.RoleClaimType = "role";
                options.TokenValidationParameters.NameClaimType = "name";
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", identityServerAuthenticationApiName);
            });
        });
        services.AddControllers();

        services.AddCustomMediator();
        services.AddInfrastructureServices();
        services.AddRepositories();
        ConfigureContext(services, configuration);

        // SignalR
        services.AddSignalR();

        services.AddSwaggerGen();
    }

    /// <summary>
    /// ConfigureContext
    /// </summary>
    private static IServiceCollection ConfigureContext(IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = ConfigHelper.GetSettingFromConfig(configuration, "ConnectionStrings", "InterviewTrainingConnection");
        return services
            .AddScoped<DbContext, InterviewContext>()
            .AddDbContext<InterviewContext>(opt => opt.UseNpgsql(connectionString));
    }
}
