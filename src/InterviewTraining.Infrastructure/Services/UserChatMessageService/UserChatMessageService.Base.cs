using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.UserChatMessage.V10;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Helpers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

///<summary>
/// Service for user chat messages
///</summary>
public partial class UserChatMessageService : IUserChatMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserChatMessageService> _logger;

    public UserChatMessageService(IUnitOfWork unitOfWork, ILogger<UserChatMessageService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

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
}
