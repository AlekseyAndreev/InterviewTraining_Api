using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CreateInterview.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetMyInterviews.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public class InterviewService : IInterviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InterviewService> _logger;

    public InterviewService(IUnitOfWork unitOfWork, ILogger<InterviewService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<GetMyInterviewsResponse> GetMyInterviewsAsync(string identityUserId, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", identityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        var interviews = await _unitOfWork.Interviews.GetByUserIdAsync(currentUser.Id, cancellationToken);

        var userTimeZone = currentUser.TimeZoneId.HasValue
            ? await _unitOfWork.TimeZones.GetByIdAsync(currentUser.TimeZoneId.Value)
            : null;

        var result = new List<InterviewDto>();

        foreach (var interview in interviews)
        {
            var interviewDate = ConvertUtcToUserTimeZone(interview.ActiveInterviewVersion.StartUtc, userTimeZone?.Code);
            var dto = new InterviewDto
            {
                Id = interview.Id,
                Status = CalculateStatus(interview.ActiveInterviewVersion),
                InterviewDate = interviewDate,
                ExpertName = interview.Expert?.FullName ?? "Не указан",
                CandidateName = interview.Candidate?.FullName ?? "Не указан"
            };
            
            result.Add(dto);
        }

        return new GetMyInterviewsResponse
        {
            Interviews = result
        };
    }

    /// <summary>
    /// Вычисление статуса интервью на основе данных версии
    /// </summary>
    private static string CalculateStatus(Domain.InterviewVersion version)
    {
        if (version == null)
            return InterviewStatus.PendingConfirmation;

        if (version.Candidate?.IsCancelled == true || version.Expert?.IsCancelled == true)
        {
            return InterviewStatus.Cancelled;
        }

        // Если оба подтвердили
        if (version.Candidate?.IsApproved == true && version.Expert?.IsApproved == true)
        {
            if (version.EndUtc.HasValue && version.EndUtc.Value < DateTime.UtcNow)
            {
                return InterviewStatus.Completed;
            }
            
            return InterviewStatus.Confirmed;
        }

        return InterviewStatus.PendingConfirmation;
    }

    /// <summary>
    /// Конвертация UTC времени в часовой пояс пользователя
    /// </summary>
    private static DateTime ConvertUtcToUserTimeZone(DateTime utcTime, string timeZoneCode)
    {
        if (string.IsNullOrEmpty(timeZoneCode) || timeZoneCode.Equals("UTC", StringComparison.OrdinalIgnoreCase))
        {
            return utcTime;
        }

        try
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);
        }
        catch (TimeZoneNotFoundException)
        {
            return utcTime;
        }
        catch (InvalidTimeZoneException)
        {
            return utcTime;
        }
    }

    public async Task<CreateInterviewResponse> CreateInterviewAsync(CreateInterviewRequest request, CancellationToken cancellationToken)
    {
        var candidate = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.CandidateId, cancellationToken);
        if (candidate == null)
        {
            throw new BusinessLogicException("Не найдена информация по текущему пользователю");
        }

        if (!candidate.IsCandidate)
        {
            throw new BusinessLogicException("Только кандидат может создать собеседование");
        }

        var expert = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.ExpertId, cancellationToken);
        if (expert == null)
        {
            throw new BusinessLogicException("Указанный эксперт не найден");
        }

        if (!expert.IsExpert)
        {
            throw new BusinessLogicException("Указанный пользователь не является экспертом");
        }

        var startUtc = ConvertUserTimeToUtc(request.Date, request.Time, candidate.TimeZone?.Code);

        var interview = new Interview
        {
            Id = Guid.NewGuid(),
            CandidateId = candidate.Id,
            ExpertId = expert.Id,
            CreatedUtc = DateTime.UtcNow
        };

        var interviewVersion = new InterviewVersion
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            StartUtc = startUtc,
            Candidate = new CandidateInterviewData
            {
                IsApproved = true,
                IsPaid = false,
                IsCancelled = false,
                Notes = request.Notes
            },
            Expert = new BaseUserInterviewData
            {
                IsApproved = false,
                IsPaid = false,
                IsCancelled = false
            },
            CreatedUtc = DateTime.UtcNow
        };

        interview.ActiveInterviewVersionId = interviewVersion.Id;
        interview.ActiveInterviewVersion = interviewVersion;

        await _unitOfWork.Interviews.AddAsync(interview);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Создано собеседование {InterviewId} кандидатом {CandidateId} с экспертом {ExpertId}",
            interview.Id, candidate.Id, expert.Id);

        return new CreateInterviewResponse
        {
            Id = interview.Id,
            Success = true
        };
    }

    /// <summary>
    /// Конвертация времени пользователя в UTC
    /// </summary>
    private static DateTime ConvertUserTimeToUtc(DateOnly date, TimeOnly time, string timeZoneCode)
    {
        var localDateTime = date.ToDateTime(time);

        if (string.IsNullOrEmpty(timeZoneCode) || timeZoneCode.Equals("UTC", StringComparison.OrdinalIgnoreCase))
        {
            return localDateTime;
        }

        try
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZoneInfo);
        }
        catch (TimeZoneNotFoundException)
        {
            // Если часовой пояс не найден, считаем что время уже в UTC
            return localDateTime;
        }
        catch (InvalidTimeZoneException)
        {
            return localDateTime;
        }
    }
}
