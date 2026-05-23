using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.Exceptions;
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
    private readonly InterviewVersionState[] CorrectStatusesToCancel = [
        InterviewVersionState.Draft,
        InterviewVersionState.PendingConfirmation,
        InterviewVersionState.ConfirmedByExpert,
        InterviewVersionState.ConfirmedByCandidate,
        InterviewVersionState.ConfirmedBothAdminApprovedTimeDidNotStart,
        InterviewVersionState.ConfirmedBothAdminNotApproved,
    ];

    /// <summary>
    /// Отменить собеседование
    /// </summary>
    public async Task<CancelInterviewResponse> CancelInterviewAsync(CancelInterviewRequest request, CancellationToken cancellationToken)
    {
        var (isCandidate, isExpert, _, interview, activeVersion, currentUser) = await GetBaseToChangeInterviewAsync(request.IdentityUserId, request.InterviewId, "Отмена собеседования", false, cancellationToken);

        var currentState = CalculateStatusWithCheck(interview, activeVersion);

        if (isCandidate)
        {
            if (!CorrectStatusesToCancel.Contains(currentState))
            {
                throw new BusinessLogicException($"Не возможно отменить собеседования кандидатом. Текущий статус собеседования {currentState}");
            }

            if (activeVersion.Candidate?.IsCancelled == true)
            {
                throw new BusinessLogicException("Вы уже отменили это собеседование");
            }
        }

        if (isExpert)
        {
            if (!CorrectStatusesToCancel.Contains(currentState))
            {
                throw new BusinessLogicException($"Не возможно отменить собеседования экспертом. Текущий статус собеседования {currentState}");
            }

            if (activeVersion.Expert?.IsCancelled == true)
            {
                throw new BusinessLogicException("Вы уже отменили это собеседование");
            }
        }

        var utcNow = DateTime.UtcNow;
        if (activeVersion.StartUtc < utcNow)
        {
            throw new BusinessLogicException("Время собеседования уже вышло. Вы уже не можете отменить собеседование");
        }

        var newVersion = CopyFrom(interview.Id, activeVersion);
        newVersion.Candidate.IsCancelled = isCandidate ? true : (activeVersion.Candidate?.IsCancelled ?? false);
        newVersion.Candidate.CancelReason = isCandidate ? request.CancelReason : activeVersion.Candidate?.CancelReason;
        newVersion.Expert.IsCancelled = isExpert ? true : (activeVersion.Expert?.IsCancelled ?? false);
        newVersion.Expert.CancelReason = isExpert ? request.CancelReason : activeVersion.Expert?.CancelReason;
        newVersion.State = CalculateStatus(interview, newVersion);

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        var cancelReasonText = string.IsNullOrEmpty(request.CancelReason) ? "без указания причины" : $"по причине: {request.CancelReason}";
        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Собеседование {InterviewId} отменено {Role} {UserId} {Reason}",
            interview.Id, userRole, currentUser.Id, cancelReasonText);

        await NotifyInterviewChanged(interview, newVersion, $"Собеседование отменено {userRole}", cancellationToken);

        return new CancelInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
