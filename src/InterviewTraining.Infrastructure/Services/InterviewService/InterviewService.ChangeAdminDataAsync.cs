using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.ChangeAdminData.V10;
using InterviewTraining.Application.Exceptions;
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
    /// Отменить собеседование
    /// </summary>
    public async Task<ChangeAdminDataResponse> ChangeAdminDataAsync(ChangeAdminDataRequest request, CancellationToken cancellationToken)
    {
        var (isCandidate, isExpert, isAdminCurrentUser, interview, activeVersion, currentUser) = await GetBaseToChangeInterviewAsync(request.CurrentIdentityUserId, request.InterviewId, "Изменение данных собеседования админом", request.IsAdmin, cancellationToken);
        
        var currentState = CalculateStatus(interview, activeVersion);

        var newVersion = CopyFrom(interview.Id, activeVersion);
        if (currentState == InterviewVersionState.ConfirmedBothAdminNotApproved)
        {
            var utcNow = DateTime.UtcNow;
            if (activeVersion.StartUtc < utcNow)
            {
                throw new BusinessLogicException("Время собеседования уже вышло. Вы уже не можете отменить собеседование");
            }
            newVersion.LinkToVideoCall = request.LinkToVideoCall;
            newVersion.Candidate.IsPaidByCandidate = request.IsPaidByCandidate == true;
        }
        if (currentState == InterviewVersionState.Completed)
        {
            newVersion.Expert.IsPaidToExpert = request.IsPaidToExpert == true;
        }
        newVersion.State = CalculateStatus(interview, newVersion);

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        if (string.IsNullOrEmpty(activeVersion.LinkToVideoCall) && !string.IsNullOrEmpty(request.LinkToVideoCall) && currentState == InterviewVersionState.ConfirmedBothAdminNotApproved)
        {
            await NotifyInterviewChanged(interview, newVersion, "Администратор добавил ссылку на созвон", cancellationToken);
        }

        if (!string.IsNullOrEmpty(activeVersion.LinkToVideoCall) && string.IsNullOrEmpty(request.LinkToVideoCall) && currentState == InterviewVersionState.ConfirmedBothAdminNotApproved)
        {
            await NotifyInterviewChanged(interview, newVersion, "Администратор удалил ссылку на созвон", cancellationToken);
        }

        if (!string.IsNullOrEmpty(activeVersion.LinkToVideoCall) && !string.IsNullOrEmpty(request.LinkToVideoCall) && activeVersion.LinkToVideoCall != request.LinkToVideoCall && currentState == InterviewVersionState.ConfirmedBothAdminNotApproved)
        {
            await NotifyInterviewChanged(interview, newVersion, "Администратор изменил ссылку на созвон", cancellationToken);
        }

        _logger.LogInformation("Собеседование {InterviewId} изменено админом {UserId}",
            interview.Id, currentUser.Id);

        return new ChangeAdminDataResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
