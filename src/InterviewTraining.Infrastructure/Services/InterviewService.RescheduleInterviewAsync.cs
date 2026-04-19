using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.RescheduleInterview.V10;
using InterviewTraining.Domain;
using Microsoft.Extensions.Logging;

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
            _logger.LogWarning("Попытка изменить время отменённого интервью {InterviewId}", interview.Id);
            throw new BusinessLogicException("Невозможно изменить время отменённого собеседования");
        }

        if (activeVersion.Candidate?.IsDeleted == true || activeVersion.Expert?.IsDeleted == true)
        {
            _logger.LogWarning("Попытка изменить время удалённого собеседования {InterviewId}", interview.Id);
            throw new BusinessLogicException("Невозможно изменить время удалённого собеседования");
        }

        var userTimeZone = currentUser.TimeZoneId.HasValue
            ? await _unitOfWork.TimeZones.GetByIdAsync(currentUser.TimeZoneId.Value)
            : null;
        var timeZoneCode = userTimeZone?.Code;

        var newStartUtc = ConvertUserTimeToUtc(request.NewDate, request.NewTime, timeZoneCode);

        if (newStartUtc < DateTime.UtcNow)
        {
            throw new BusinessLogicException("Нельзя переносить собеседование в прошлое");
        }

        var newVersion = new InterviewVersion
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            StartUtc = newStartUtc,
            EndUtc = activeVersion.EndUtc,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            LanguageId = activeVersion.LanguageId,
            CreatedUtc = DateTime.UtcNow,
            IsAdminApproved = activeVersion.IsAdminApproved,
            CurrencyId = activeVersion.CurrencyId,
            InterviewPrice = activeVersion.InterviewPrice,
            Candidate = new CandidateInterviewData
            {
                IsRescheduled = isCandidate,
                IsApproved = isCandidate,
                IsPaidByCandidate = activeVersion.Candidate?.IsPaidByCandidate ?? false,
                IsCancelled = activeVersion.Candidate?.IsCancelled ?? false,
                CancelReason = activeVersion.Candidate?.CancelReason,
                IsDeleted = activeVersion.Candidate?.IsDeleted ?? false,
            },
            Expert = new ExpertInterviewData
            {
                IsRescheduled = isExpert,
                IsApproved = isExpert,
                IsPaidToExpert = activeVersion.Expert?.IsPaidToExpert ?? false,
                IsCancelled = activeVersion.Expert?.IsCancelled ?? false,
                CancelReason = activeVersion.Expert?.CancelReason,
                IsDeleted = activeVersion.Expert?.IsDeleted ?? false,
            }
        };

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Время собеседования {InterviewId} изменено {Role} {UserId} на {NewTime}",
            interview.Id, userRole, currentUser.Id, newStartUtc);

        return new RescheduleInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
