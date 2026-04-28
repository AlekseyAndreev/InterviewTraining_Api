using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Application.UserChatMessage.V10;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Helpers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

///<summary>
/// Service for user chat messages
///</summary>
public partial class UserChatMessageService(IUnitOfWork _unitOfWork, IUserWithAdminChatNotificationProvider _userWithAdminChatNotificationProvider, ILogger<UserChatMessageService> _logger) : IUserChatMessageService
{
    private static UserChatMessageDto MapToDto(UserChatMessage message, string userTimeZoneCode) => new()
    {
        Id = message.Id,
        SenderUserId = message.SenderUser?.IdentityUserId,
        SenderFullName = message.SenderUser?.FullName,
        ReceiverUserId = message.ReceiverUser?.IdentityUserId,
        ReceiverFullName = message.ReceiverUser?.FullName,
        MessageText = message.MessageText,
        IsEdited = message.IsEdited,
        IsRead = message.IsRead,
        Created = DateTimeHelper.ConvertUtcToUserTimeZone(message.CreatedUtc, userTimeZoneCode),
    };

    private async Task NotifyUserWithAdminChatChanged(UserChatMessage userChatMessage, string userId, CancellationToken cancellationToken)
    {
        await _userWithAdminChatNotificationProvider.NotifyChatMessageUpdatedAsync(new UserWithAdminChatMessageNotificationDto
        {
            Id = userChatMessage.Id,
            CreatedUtc = userChatMessage.CreatedUtc,
            ModifiedUtc = userChatMessage.ModifiedUtc,
            IsEdited = userChatMessage.IsEdited,
            Text = userChatMessage.MessageText,
        });
    }
}
