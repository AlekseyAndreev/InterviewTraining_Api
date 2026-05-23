using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.ConfirmInterview.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
    private readonly InterviewVersionState[] CorrectStatusesToConfirmForExpert = [InterviewVersionState.PendingConfirmation, InterviewVersionState.ConfirmedByCandidate];
    private readonly InterviewVersionState[] CorrectStatusesToConfirmForCandidate = [InterviewVersionState.PendingConfirmation, InterviewVersionState.ConfirmedByExpert];

    /// <summary>
    /// Подтвердить собеседование
    /// </summary>
    public async Task<ConfirmInterviewResponse> ConfirmInterviewAsync(ConfirmInterviewRequest request, CancellationToken cancellationToken)
    {
        var (isCandidate, isExpert, isAdminCurrentUser, interview, activeVersion, currentUser) = await GetBaseToChangeInterviewAsync(request.IdentityUserId, request.InterviewId, "Подтверждение собеседования", request.IsAdmin, cancellationToken);
        
        var currentState = CalculateStatusWithCheck(interview, activeVersion);

        if (isCandidate)
        {
            if (!CorrectStatusesToConfirmForCandidate.Contains(currentState))
            {
                throw new BusinessLogicException($"Не возможно подтвердить статус собеседования кандидатом. Текущий статус собеседования {currentState}");
            }

            if (activeVersion.Candidate?.IsApproved == true)
            {
                throw new BusinessLogicException("Вы уже подтвердили это собеседование");
            }
        }

        if (isExpert)
        {
            if (!CorrectStatusesToConfirmForExpert.Contains(currentState))
            {
                throw new BusinessLogicException($"Не возможно подтвердить статус собеседования экспертом. Текущий статус собеседования {currentState}");
            }

            if (activeVersion.Expert?.IsApproved == true)
            {
                throw new BusinessLogicException("Вы уже подтвердили это собеседование");
            }
        }

        if (isAdminCurrentUser)
        {
            if (activeVersion.State != InterviewVersionState.ConfirmedBothAdminNotApproved)
            {
                throw new BusinessLogicException("Админ не может подтвердить собеседование. Текущий статус должен быть ConfirmedBothAdminNotApproved");
            }

            if (string.IsNullOrEmpty(activeVersion.LinkToVideoCall))
            {
                throw new BusinessLogicException("Админ не может подтвердить собеседование. Нет ссылки на созвон");
            }

            if (!activeVersion.Candidate.IsPaidByCandidate)
            {
                throw new BusinessLogicException("Админ не может подтвердить собеседование. Собеседование не оплачено кандидатом");
            }

            if (activeVersion.IsAdminApproved)
            {
                throw new BusinessLogicException("Админ уже подтвердили это собеседование");
            }
        }

        var utcNow = DateTime.UtcNow;
        if (activeVersion.StartUtc < utcNow)
        {
            throw new BusinessLogicException("Время собеседования уже вышло. Вы уже не можете подтвердить своё участие");
        }

        var newVersion = InterviewHelper.CopyFrom(interview.Id, activeVersion);
        newVersion.Candidate.IsApproved = isCandidate ? true : (activeVersion.Candidate?.IsApproved ?? false);
        newVersion.Expert.IsApproved = isExpert ? true : (activeVersion.Expert?.IsApproved ?? false);
        newVersion.IsAdminApproved = isAdminCurrentUser ? true : activeVersion.IsAdminApproved;
        newVersion.State = InterviewHelper.CalculateStatus(interview, newVersion);
        newVersion.ChangedBy = GetChangedBy(isCandidate, isExpert, isAdminCurrentUser);

        await _unitOfWork.InterviewVersions.AddAsync(newVersion, cancellationToken);

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
