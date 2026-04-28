using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Infrastructure.Providers;
using InterviewTraining.Infrastructure.Repositories;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using InterviewTraining.Infrastructure.Services;
using InterviewTraining.Infrastructure.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTraining.Infrastructure;

///<summary>
/// Startup
///</summary>
public static class ServiceCollectionExtensions
{
    ///<summary>
    /// Add infrastructure services
    ///</summary>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IExpertService, ExpertService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserSkillService, UserSkillService>();
        services.AddScoped<IUserAvailableTimeService, UserAvailableTimeService>();
        services.AddScoped<IInterviewService, InterviewService>();
        services.AddScoped<IInterviewChatMessageService, InterviewChatMessageService>();
        services.AddScoped<IInterviewLanguageService, InterviewLanguageService>();
        services.AddScoped<IUserSyncService, UserSyncService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IUserNotificationService, UserNotificationService>();
        services.AddScoped<IUserChatMessageService, UserChatMessageService>();
        services.AddInfrastructureProviders();
        return services;
    }

    ///<summary>
    /// Add repositories
    ///</summary>
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
        services.AddScoped<IUserChatMessageRepository, UserChatMessageRepository>();
        return services;
    }

    private static IServiceCollection AddInfrastructureProviders(this IServiceCollection services)
    {
        services.AddScoped<IInterviewNotificationProvider, InterviewNotificationProvider>();
        services.AddScoped<IUserTimeZoneProvider, UserTimeZoneProvider>();
        services.AddScoped<IInterviewChatMessageProvider, InterviewChatMessageProvider>();
        services.AddScoped<IUserWithAdminChatNotificationProvider, UserWithAdminChatNotificationProvider>();
        return services;
    }
}
