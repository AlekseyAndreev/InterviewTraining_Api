using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.Exceptions;
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

        var newVersion = new InterviewVersion
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            StartUtc = activeVersion.StartUtc,
            EndUtc = activeVersion.EndUtc,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            LanguageId = activeVersion.LanguageId,
            CreatedUtc = DateTime.UtcNow,
            Candidate = new CandidateInterviewData
            {
                IsApproved = activeVersion.Candidate?.IsApproved ?? false,
                IsPaid = activeVersion.Candidate?.IsPaid ?? false,
                IsCancelled = isCandidate ? true : (activeVersion.Candidate?.IsCancelled ?? false),
                CancellReason = isCandidate ? request.CancelReason : activeVersion.Candidate?.CancellReason,
                Notes = activeVersion.Candidate?.Notes
            },
            Expert = new BaseUserInterviewData
            {
                IsApproved = activeVersion.Expert?.IsApproved ?? false,
                IsPaid = activeVersion.Expert?.IsPaid ?? false,
                IsCancelled = isExpert ? true : (activeVersion.Expert?.IsCancelled ?? false),
                CancellReason = isExpert ? request.CancelReason : activeVersion.Expert?.CancellReason
            }
        };

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

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
