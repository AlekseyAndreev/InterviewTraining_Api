using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CreateInterview.V10;
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
    public async Task<CreateInterviewResponse> CreateInterviewAsync(CreateInterviewRequest request, CancellationToken cancellationToken)
    {
        var candidate = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.CandidateId, cancellationToken);
        if (candidate == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.CandidateId);
            throw new BusinessLogicException("Не найдена информация по текущему пользователю");
        }

        if (!candidate.IsCandidate)
        {
            _logger.LogWarning("Пользователь {UserId} не является кандидатом", request.CandidateId);
            throw new BusinessLogicException("Только кандидат может создать собеседование");
        }

        var expert = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.ExpertId, cancellationToken);
        if (expert == null)
        {
            _logger.LogWarning("Не найден эксперт с идентификатором {ExpertId}", request.ExpertId);
            throw new BusinessLogicException("Указанный эксперт не найден");
        }

        if (!expert.IsExpert)
        {
            _logger.LogWarning("Пользователь {ExpertId} не является экспертом", request.ExpertId);
            throw new BusinessLogicException("Указанный пользователь не является экспертом");
        }

        if (expert.Id == candidate.Id)
        {
            throw new BusinessLogicException("Эксперт и кандидат один и тот же пользователь");
        }

        if (expert.IdentityUserId == candidate.IdentityUserId)
        {
            throw new BusinessLogicException("Эксперт и кандидат один и тот же пользователь");
        }

        var interviewLanguageId = await GetLanguageId(request.InterviewLanguageId);

        var startUtc = DateTimeHelper.ConvertUserTimeToUtc(request.Date, request.Time, candidate.TimeZone?.Code);

        if (startUtc < DateTime.UtcNow)
        {
            throw new BusinessLogicException("Нельзя создавать собеседование в прошлом");
        }

        var interview = new Interview
        {
            Id = Guid.NewGuid(),
            CandidateId = candidate.Id,
            ExpertId = expert.Id,
            ActiveInterviewVersionId = null,
            CreatedUtc = DateTime.UtcNow
        };

        await _unitOfWork.Interviews.AddAsync(interview);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var interviewVersion = CreateNewEmptyVersion(interview.Id, startUtc, expert.InterviewPrice, expert.CurrencyId, interviewLanguageId);

        await interviewChatMessageProvider.CreateInterviewChatMessage(interview.Id, MessageSenderType.Candidate, candidate.Id, request.Notes, cancellationToken);

        await _unitOfWork.InterviewVersions.AddAsync(interviewVersion);

        interview.ActiveInterviewVersionId = interviewVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Создано собеседование {InterviewId} кандидатом {CandidateId} с экспертом {ExpertId}",
            interview.Id, candidate.Id, expert.Id);

        return new CreateInterviewResponse
        {
            Id = interview.Id,
            Success = true
        };
    }

    private async Task<Guid?> GetLanguageId(Guid? interviewLanguageIdParam)
    {
        if (!interviewLanguageIdParam.HasValue)
        {
            return null;
        }

        var existsLang = await _unitOfWork.InterviewLanguages.AnyAsync(x => x.Id == interviewLanguageIdParam.Value && !x.IsDeleted);
        if (!existsLang)
        {
            throw new BusinessLogicException("Язык собеседования не найден в БД");
        }

        return interviewLanguageIdParam.Value;
    }

    private static InterviewVersion CreateNewEmptyVersion(Guid interviewId, DateTime startUtc, decimal? expertInterviewPrice, Guid? expertCurrencyId, Guid? interviewLanguageId)
    {
        return new InterviewVersion
        {
            Id = Guid.NewGuid(),
            InterviewId = interviewId,
            StartUtc = startUtc,
            InterviewPrice = expertInterviewPrice,
            CurrencyId = expertCurrencyId,
            Candidate = new CandidateInterviewData
            {
                IsApproved = true,
                IsPaidByCandidate = false,
                IsRescheduled = false,
                IsDeleted = false,
                CancelReason = null,
                IsCancelled = false,
            },
            Expert = new ExpertInterviewData
            {
                IsApproved = false,
                IsPaidToExpert = false,
                IsCancelled = false,
                IsRescheduled = false,
                IsDeleted = false,
                CancelReason = null,
            },
            CreatedUtc = DateTime.UtcNow,
            LanguageId = interviewLanguageId,
        };
    }
}
