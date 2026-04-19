using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Common;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetInterviewInfo.V10;
using InterviewTraining.Domain;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
    /// <summary>
    /// Получить детальную информацию по собеседованию
    /// </summary>
    public async Task<GetInterviewInfoResponse> GetInterviewInfoAsync(GetInterviewInfoRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        var interview = await _unitOfWork.Interviews.GetWithDetailsAsync(request.InterviewId, cancellationToken);
        if (interview == null)
        {
            _logger.LogWarning("Интервью с идентификатором {InterviewId} не найдено", request.InterviewId);
            throw new EntityNotFoundException("Собеседование не найдено");
        }

        var hasAccess = request.IsAdmin
            || interview.CandidateId == currentUser.Id
            || interview.ExpertId == currentUser.Id;

        if (!hasAccess)
        {
            _logger.LogWarning("Пользователь {UserId} не имеет доступа к интервью {InterviewId}",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("У вас нет доступа к этому собеседованию");
        }

        var activeVersion = interview.ActiveInterviewVersion;

        if (activeVersion == null)
        {
            throw new BusinessLogicException("Не задана активная версия для интервью");
        }

        var userTimeZone = currentUser.TimeZoneId.HasValue
            ? await _unitOfWork.TimeZones.GetByIdAsync(currentUser.TimeZoneId.Value)
            : null;
        var timeZoneCode = userTimeZone?.Code;

        var status = CalculateStatus(interview, activeVersion);

        var response = new GetInterviewInfoResponse
        {
            Id = interview.Id,
            Status = status,
            StatusDescriptionRu = InterviewStatusDescription.GetStatusDescriptionRu(status),
            StatusDescriptionEn = InterviewStatusDescription.GetStatusDescriptionEn(status),
            StartDateTime = ConvertUtcToUserTimeZone(activeVersion.StartUtc, timeZoneCode),
            EndDateTime = activeVersion.EndUtc.HasValue == true
                ? ConvertUtcToUserTimeZone(activeVersion.EndUtc.Value, timeZoneCode)
                : null,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            InterviewPrice = activeVersion.InterviewPrice,
            CurrencyNameEn = activeVersion.Currency?.NameEn,
            CurrencyNameRu = activeVersion.Currency?.NameRu,

            CreatedUtc = ConvertUtcToUserTimeZone(interview.CreatedUtc, timeZoneCode),
            Candidate = MapParticipant(interview.Candidate),
            Expert = MapParticipant(interview.Expert),
            Language = activeVersion.Language != null ? MapLanguage(activeVersion.Language) : null,
            CandidateApproval = activeVersion.Candidate != null
                ? MapApproval(activeVersion.Candidate)
                : null,
            ExpertApproval = activeVersion.Expert != null
                ? MapApproval(activeVersion.Expert)
                : null
        };

        return response;
    }

    private static ChatMessageFrom MapMessageSenderType(MessageSenderType messageSenderType) =>
        messageSenderType switch
        {
            MessageSenderType.Admin => ChatMessageFrom.Admin,
            MessageSenderType.Candidate => ChatMessageFrom.Candidate,
            MessageSenderType.Expert => ChatMessageFrom.Expert,
            MessageSenderType.System => ChatMessageFrom.System,
            _ => ChatMessageFrom.Unknown,
        };

    /// <summary>
    /// Маппинг участника интервью
    /// </summary>
    private static InterviewParticipantDto MapParticipant(AdditionalUserInfo user)
    {
        if (user == null)
        {
            return null;
        }

        return new InterviewParticipantDto
        {
            Id = user.Id,
            IdentityUserId = user.IdentityUserId,
            FullName = user.FullName ?? "Не указан",
            Photo = user.PhotoLocal,
            ShortDescription = user.ShortDescription
        };
    }

    /// <summary>
    /// Маппинг языка интервью
    /// </summary>
    private static InterviewLanguageDto MapLanguage(InterviewLanguage language)
    {
        if (language == null)
        {
            return null;
        }

        return new InterviewLanguageDto
        {
            Id = language.Id,
            Code = language.Code,
            NameRu = language.NameRu,
            NameEn = language.NameEn
        };
    }

    /// <summary>
    /// Маппинг данных подтверждения
    /// </summary>
    private static ParticipantApprovalDto MapApproval(BaseUserInterviewData data)
    {
        if (data == null)
        {
            return null;
        }

        return new ParticipantApprovalDto
        {
            IsRescheduled = data.IsRescheduled,
            IsApproved = data.IsApproved,
            IsCancelled = data.IsCancelled,
            CancelReason = data.CancelReason
        };
    }
}
