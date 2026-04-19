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

        var isCandidate = interview.CandidateId == currentUser.Id;
        var isExpert = interview.ExpertId == currentUser.Id;

        if (!isCandidate && !isExpert)
        {
            _logger.LogWarning("Пользователь {UserId} не является участником интервью {InterviewId}",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("Вы не являетесь участником этого собеседования");
        }

        var activeVersion = interview.ActiveInterviewVersion;
        if (activeVersion == null)
        {
            throw new BusinessLogicException("Не задана активная версия для интервью");
        }

        if (activeVersion.Candidate?.IsDeleted == true || activeVersion.Expert?.IsDeleted == true)
        {
            _logger.LogWarning("Попытка отменить удалённое интервью {InterviewId}", interview.Id);
            throw new BusinessLogicException("Невозможно отменить удалённое собеседование");
        }

        if (isCandidate && activeVersion.Expert?.IsCancelled == true)
        {
            _logger.LogWarning("Кандидат {UserId} пытается отменить интервью {InterviewId}, которое уже отменено экспертом",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("Собеседование уже отменено экспертом. Вы не можете его отменить.");
        }

        if (isExpert && activeVersion.Candidate?.IsCancelled == true)
        {
            _logger.LogWarning("Эксперт {UserId} пытается отменить интервью {InterviewId}, которое уже отменено кандидатом",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("Собеседование уже отменено кандидатом. Вы не можете его отменить.");
        }

        if (isCandidate && activeVersion.Candidate?.IsCancelled == true)
        {
            throw new BusinessLogicException("Вы уже отменили это собеседование");
        }

        if (isExpert && activeVersion.Expert?.IsCancelled == true)
        {
            throw new BusinessLogicException("Вы уже отменили это собеседование");
        }

        var utcNow = DateTime.UtcNow;
        if (activeVersion.StartUtc < utcNow)
        {
            throw new BusinessLogicException("Время собеседования уже вышло. Вы уже не можете отменить собеседование");
        }

        var newVersion = new InterviewVersion
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            StartUtc = activeVersion.StartUtc,
            EndUtc = activeVersion.EndUtc,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            LanguageId = activeVersion.LanguageId,
            CreatedUtc = DateTime.UtcNow,
            IsAdminApproved = activeVersion.IsAdminApproved,
            CurrencyId = activeVersion.CurrencyId,
            InterviewPrice = activeVersion.InterviewPrice,
            Candidate = new CandidateInterviewData
            {
                IsApproved = activeVersion.Candidate?.IsApproved ?? false,
                IsPaidByCandidate = activeVersion.Candidate?.IsPaidByCandidate ?? false,
                IsCancelled = isCandidate ? true : (activeVersion.Candidate?.IsCancelled ?? false),
                CancelReason = isCandidate ? request.CancelReason : activeVersion.Candidate?.CancelReason,
                IsDeleted = activeVersion.Candidate?.IsDeleted ?? false,
                IsRescheduled = activeVersion.Candidate?.IsRescheduled ?? false,
            },
            Expert = new ExpertInterviewData
            {
                IsApproved = activeVersion.Expert?.IsApproved ?? false,
                IsPaidToExpert = activeVersion.Expert?.IsPaidToExpert ?? false,
                IsCancelled = isExpert ? true : (activeVersion.Expert?.IsCancelled ?? false),
                CancelReason = isExpert ? request.CancelReason : activeVersion.Expert?.CancelReason,
                IsDeleted = activeVersion.Expert?.IsDeleted ?? false,
                IsRescheduled = activeVersion.Expert?.IsRescheduled ?? false,
            }
        };

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        // Отправляем уведомление через SignalR
        await _notificationService.NotifyInterviewVersionChangedAsync(new InterviewVersionNotificationDto
        {
            InterviewId = interview.Id,
            VersionId = newVersion.Id,
            ChangeType = InterviewChangeType.Cancelled,
            StartUtc = newVersion.StartUtc,
            EndUtc = newVersion.EndUtc,
            CandidateApproved = newVersion.Candidate?.IsApproved ?? false,
            ExpertApproved = newVersion.Expert?.IsApproved ?? false,
            CandidateCancelled = newVersion.Candidate?.IsCancelled ?? false,
            ExpertCancelled = newVersion.Expert?.IsCancelled ?? false,
            CancelReason = isCandidate ? request.CancelReason : newVersion.Expert?.CancelReason
        });

        var cancelReasonText = string.IsNullOrEmpty(request.CancelReason) ? "без указания причины" : $"по причине: {request.CancelReason}";
        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Собеседование {InterviewId} отменено {Role} {UserId} {Reason}",
            interview.Id, userRole, currentUser.Id, cancelReasonText);

        return new CancelInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
