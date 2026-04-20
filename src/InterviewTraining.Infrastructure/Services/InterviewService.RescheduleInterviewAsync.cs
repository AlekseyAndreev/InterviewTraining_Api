using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.RescheduleInterview.V10;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
    /// <summary>
    /// Изменить время собеседования
    /// </summary>
    public async Task<RescheduleInterviewResponse> RescheduleInterviewAsync(RescheduleInterviewRequest request, CancellationToken cancellationToken)
    {
        var (isCandidate, isExpert, interview, activeVersion, currentUser) = await GetBaseToChangeInterviewAsync(request.IdentityUserId, request.InterviewId, "Перенос времени", cancellationToken);

        var timeZoneCode = await GetTimeZoneCode(currentUser.TimeZoneId);
        var newStartUtc = ConvertUserTimeToUtc(request.NewDate, request.NewTime, timeZoneCode);
        if (newStartUtc < DateTime.UtcNow)
        {
            throw new BusinessLogicException("Нельзя переносить собеседование в прошлое");
        }

        var newVersion = CopyFrom(interview.Id, activeVersion);
        newVersion.Candidate.IsRescheduled = isCandidate;
        newVersion.Candidate.IsApproved = isCandidate;
        newVersion.Expert.IsRescheduled = isExpert;
        newVersion.Expert.IsApproved = isExpert;
        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        await _notificationService.NotifyInterviewVersionChangedAsync(new InterviewVersionNotificationDto
        {
            InterviewId = interview.Id,
            VersionId = newVersion.Id,
            ChangeType = InterviewChangeType.Rescheduled,
            StartUtc = newVersion.StartUtc,
            EndUtc = newVersion.EndUtc,
            CandidateApproved = newVersion.Candidate?.IsApproved ?? false,
            ExpertApproved = newVersion.Expert?.IsApproved ?? false,
            CandidateCancelled = newVersion.Candidate?.IsCancelled ?? false,
            ExpertCancelled = newVersion.Expert?.IsCancelled ?? false
        });

        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Время собеседования {InterviewId} изменено {Role} {UserId} на {NewTime}",
            interview.Id, userRole, currentUser.Id, newStartUtc);

        var message = $"Время собеседования изменено {userRole}";
        await CreateChatMessageInternal(interview.Id, MessageSenderType.System, null, message, cancellationToken);

        return new RescheduleInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
