using InterviewTraining.Application.UpdateUserSkills.V10;
using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetAllExperts.V10;
using InterviewTraining.Application.GetSkillsTree.V10;
using InterviewTraining.Application.GetUserInfo.V10;
using InterviewTraining.Application.UpdateUserInfo.V10;
using InterviewTraining.Application.ManageAvailableTime.V10;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTraining.Application;

/// <summary>
/// Startup
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    ///<param name="services"></param>
    public static IServiceCollection AddCustomMediator(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomMediator, CustomMediator>()
            .AddScoped<IMediatorHandler<GetAllExpertsRequest, GetAllExpertsResponse>, GetAllExpertsHandler>()
            .AddScoped<IMediatorHandler<GetUserInfoRequest, GetUserInfoResponse>, GetUserInfoHandler>()
            .AddScoped<IMediatorHandler<UpdateUserInfoRequest, UpdateUserInfoResponse>, UpdateUserInfoHandler>()
            .AddScoped<IMediatorHandler<GetSkillsTreeRequest, GetSkillsTreeResponse>, GetSkillsTreeHandler>()
            .AddScoped<IMediatorHandler<UpdateUserSkillsRequest, UpdateUserSkillsResponse>, UpdateUserSkillsHandler>()
            .AddScoped<IMediatorHandler<CreateAvailableTimeRequest, CreateAvailableTimeResponse>, CreateAvailableTimeHandler>()
            .AddScoped<IMediatorHandler<UpdateAvailableTimeRequest, UpdateAvailableTimeResponse>, UpdateAvailableTimeHandler>()
            .AddScoped<IMediatorHandler<GetAvailableTimeRequest, GetAvailableTimeResponse>, GetAvailableTimeHandler>()
            .AddScoped<IMediatorHandler<DeleteAvailableTimeRequest, DeleteAvailableTimeResponse>, DeleteAvailableTimeHandler>();
    }
}
