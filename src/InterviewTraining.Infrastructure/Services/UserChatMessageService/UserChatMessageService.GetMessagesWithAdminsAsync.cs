using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.UserChatMessage.V10.GetUserChatMessagesWithAdmins;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

///<summary>
/// Service for user chat messages
///</summary>
public partial class UserChatMessageService
{
    ///<summary>
    /// Get messages between user and administrators
    ///</summary>
    public async Task<GetUserChatMessagesWithAdminsResponse> GetMessagesWithAdminsAsync(
        GetUserChatMessagesWithAdminsRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("User not found");
        }

        // Get all admin users
        var adminUsers = await _unitOfWork.AdditionalUserInfos.GetAllAsync();
        var adminIds = adminUsers
            .Where(u => u.IsExpert && !u.IsCandidate)
            .Select(u => u.Id)
            .ToList();

        var messages = await _unitOfWork.UserChatMessages.GetMessagesWithAdminsAsync(
            user.Id, cancellationToken);

        var messageList = messages.ToList();

        var userTimeZoneCode = user.TimeZone?.Code;

        return new GetUserChatMessagesWithAdminsResponse
        {
            Messages = messageList.Select(x => MapToDto(x, userTimeZoneCode)).ToList(),
        };
    }
}
