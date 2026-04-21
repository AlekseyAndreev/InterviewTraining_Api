using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.GetUserNotifications.V10;

public class GetUserNotificationsRequest : IMediatorRequest<GetUserNotificationsResponse>
{
    public string IdenitityUserId { get; set; }
}
