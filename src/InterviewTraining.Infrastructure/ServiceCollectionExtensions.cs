using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Infrastructure.Repositories;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using InterviewTraining.Infrastructure.Services;
using InterviewTraining.Infrastructure.SignalR;
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
        services.AddScoped<IExpertService, ExpertService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserSkillService, UserSkillService>();
        services.AddScoped<IUserAvailableTimeService, UserAvailableTimeService>();
        services.AddScoped<IInterviewService, InterviewService>();
        services.AddScoped<IInterviewLanguageService, InterviewLanguageService>();
        services.AddScoped<IUserSyncService, UserSyncService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IInterviewNotificationService, InterviewNotificationService>();
        services.AddScoped<IUserNotificationService, UserNotificationService>();
        return services;
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    ///<param name="services"></param>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ISkillGroupRepository, SkillGroupRepository>();
        services.AddScoped<ISkillTagRepository, SkillTagRepository>();
        services.AddScoped<IUserRatingRepository, UserRatingRepository>();
        services.AddScoped<IAdditionalUserInfoRepository, AdditionalUserInfoRepository>();
        services.AddScoped<ITimeZoneRepository, TimeZoneRepository>();
        services.AddScoped<IUserSkillRepository, UserSkillRepository>();
        services.AddScoped<IUserAvailableTimeRepository, UserAvailableTimeRepository>();
        services.AddScoped<IInterviewRepository, InterviewRepository>();
        services.AddScoped<IInterviewVersionRepository, InterviewVersionRepository>();
        services.AddScoped<IInterviewLanguageRepository, InterviewLanguageRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
        return services;
    }
}
