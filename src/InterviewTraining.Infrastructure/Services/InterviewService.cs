using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetMyInterviews.V10;
using InterviewTraining.Application.Interfaces;
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
        // Получаем информацию о текущем пользователе
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", identityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        // Получаем все интервью пользователя
        var interviews = await _unitOfWork.Interviews.GetByUserIdAsync(currentUser.Id, cancellationToken);

        // Получаем часовой пояс пользователя (или UTC по умолчанию)
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

        // Если отменено кем-то
        if (version.Candidate?.IsCancelled == true || version.Expert?.IsCancelled == true)
        {
            return InterviewStatus.Cancelled;
        }

        // Если оба подтвердили
        if (version.Candidate?.IsApproved == true && version.Expert?.IsApproved == true)
        {
            // Проверяем, прошло ли интервью
            if (version.EndUtc.HasValue && version.EndUtc.Value < DateTime.UtcNow)
            {
                return InterviewStatus.Completed;
            }
            
            return InterviewStatus.Confirmed;
        }

        // Ожидает подтверждения
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
            // Если часовой пояс не найден, возвращаем UTC
            return utcTime;
        }
        catch (InvalidTimeZoneException)
        {
            return utcTime;
        }
    }
}
