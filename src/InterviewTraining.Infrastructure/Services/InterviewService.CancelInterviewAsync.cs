using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.SignalR;
using InterviewTraining.Domain;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
    /// <summary>
    /// Отменить собеседование
    /// </summary>
    public async Task<CancelInterviewResponse> CancelInterviewAsync(CancelInterviewRequest request, CancellationToken cancellationToken)
    {
        var (isCandidate, isExpert, interview, activeVersion, currentUser) = await GetBaseToChangeInterviewAsync(request.IdentityUserId, request.InterviewId, "Отмена собеседования", cancellationToken);

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
