using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.ConfirmInterview.V10;
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
    /// Подтвердить собеседование
    /// </summary>
    public async Task<ConfirmInterviewResponse> ConfirmInterviewAsync(ConfirmInterviewRequest request, CancellationToken cancellationToken)
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

        if (activeVersion.Candidate?.IsCancelled == true || activeVersion.Expert?.IsCancelled == true)
        {
            _logger.LogWarning("Попытка подтвердить отменённое интервью {InterviewId}", interview.Id);
            throw new BusinessLogicException("Невозможно подтвердить отменённое собеседование");
        }

        if (isCandidate && activeVersion.Candidate?.IsApproved == true)
        {
            throw new BusinessLogicException("Вы уже подтвердили это собеседование");
        }

        if (isExpert && activeVersion.Expert?.IsApproved == true)
        {
            throw new BusinessLogicException("Вы уже подтвердили это собеседование");
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
                IsApproved = isCandidate ? true : (activeVersion.Candidate?.IsApproved ?? false),
                IsPaid = activeVersion.Candidate?.IsPaid ?? false,
                IsCancelled = activeVersion.Candidate?.IsCancelled ?? false,
                CancellReason = activeVersion.Candidate?.CancellReason,
                Notes = activeVersion.Candidate?.Notes
            },
            Expert = new BaseUserInterviewData
            {
                IsApproved = isExpert ? true : (activeVersion.Expert?.IsApproved ?? false),
                IsPaid = activeVersion.Expert?.IsPaid ?? false,
                IsCancelled = activeVersion.Expert?.IsCancelled ?? false,
                CancellReason = activeVersion.Expert?.CancellReason
            }
        };

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Собеседование {InterviewId} подтверждено {Role} {UserId}",
            interview.Id, userRole, currentUser.Id);

        return new ConfirmInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
