using System.Linq;
using InterviewTraining.Application.CustomMediatorLogic;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTraining.Application;

/// <summary>
/// Startup
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// Автоматически сканирует сборку и регистрирует все реализации IMediatorHandler.
    /// </summary>
    ///<param name="services"></param>
    public static IServiceCollection AddCustomMediator(this IServiceCollection services)
    {
        services.AddScoped<ICustomMediator, CustomMediator>();

        var handlerInterfaceType = typeof(IMediatorHandler<,>);
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        var handlerTypes = assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Select(t => new
            {
                ImplementationType = t,
                InterfaceType = t.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
            })
            .Where(x => x.InterfaceType != null);

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.InterfaceType, handler.ImplementationType);
        }

        return services;
    }
}
