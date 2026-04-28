using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.UserChatMessage.V10.GetMessagesForExactUserToAdmin;
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
    public async Task<GetMessagesForExactUserToAdminResponse> GetMessagesForExactUserToAdminAsync(
        GetMessagesForExactUserToAdminRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.CurrentIdentityUserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", request.CurrentIdentityUserId);
            throw new BusinessLogicException("User not found");
        }

        if(!request.IsAdmin)
        {
            _logger.LogError("Current user not admin: {UserId}", request.CurrentIdentityUserId);
            throw new BusinessLogicException("User must be admin");
        }

        var chatWithIdentityUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.ChatWithIdentityUserId, cancellationToken);
        if (chatWithIdentityUser == null)
        {
            _logger.LogWarning("User not found: {UserId}", request.ChatWithIdentityUserId);
            throw new BusinessLogicException("User not found");
        }

        var messages = await _unitOfWork.UserChatMessages.GetMessagesWithAdminsAsync(
            chatWithIdentityUser.Id, cancellationToken);

        var messageList = messages.ToList();

        var userTimeZoneCode = user.TimeZone?.Code;

        return new GetMessagesForExactUserToAdminResponse
        {
            Messages = messageList.Select(x => MapToDto(x, userTimeZoneCode)).ToList(),
        };
    }
}
