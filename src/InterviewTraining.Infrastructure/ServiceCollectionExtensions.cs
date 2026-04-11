using InterviewTraining.Application.Interfaces;
using InterviewTraining.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTraining.Infrastructure;

/// <summary>
/// Startup
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    ///<param name="services"></param>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IExpertService, ExpertService>();
    }
}
