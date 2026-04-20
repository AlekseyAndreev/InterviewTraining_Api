using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.ConfirmInterview.V10;
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
    /// Подтвердить собеседование
    /// </summary>
    public async Task<ConfirmInterviewResponse> ConfirmInterviewAsync(ConfirmInterviewRequest request, CancellationToken cancellationToken)
    {
        var (isCandidate, isExpert, interview, activeVersion, currentUser) = await GetBaseToChangeInterviewAsync(request.IdentityUserId, request.InterviewId, "Подтверждение собеседования", cancellationToken);

        if (isCandidate && activeVersion.Candidate?.IsApproved == true)
        {
            throw new BusinessLogicException("Вы уже подтвердили это собеседование");
        }

        if (isExpert && activeVersion.Expert?.IsApproved == true)
        {
            throw new BusinessLogicException("Вы уже подтвердили это собеседование");
        }

        var utcNow = DateTime.UtcNow;
        if (activeVersion.StartUtc < utcNow)
        {
            throw new BusinessLogicException("Время собеседования уже вышло. Вы уже не можете подтвердить своё участие");
        }

        var newVersion = CopyFrom(interview.Id, activeVersion);
        newVersion.Candidate.IsApproved = isCandidate ? true : (activeVersion.Candidate?.IsApproved ?? false);
        newVersion.Expert.IsApproved = isExpert ? true : (activeVersion.Expert?.IsApproved ?? false);

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Собеседование {InterviewId} подтверждено {Role} {UserId}",
            interview.Id, userRole, currentUser.Id);

        await NotifyInterviewChanged(interview, newVersion, $"Собеседование подтверждено {userRole}", cancellationToken);

        return new ConfirmInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
