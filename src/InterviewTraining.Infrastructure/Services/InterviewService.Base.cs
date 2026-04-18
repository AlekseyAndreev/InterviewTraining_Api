using InterviewTraining.Application.Common;
using InterviewTraining.Application.GetMyInterviews.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService : IInterviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InterviewService> _logger;

    public InterviewService(IUnitOfWork unitOfWork, ILogger<InterviewService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
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

    /// <summary>
    /// Вычисление статуса интервью на основе данных версии
    /// </summary>
    private static string CalculateStatus(Interview interview, InterviewVersion version)
    {
        if (version == null)
        {
            return InterviewStatus.Draft;
        }

        if (version.Candidate?.IsCancelled == true && version.Expert?.IsCancelled == true)
        {
            return InterviewStatus.CancelledByCandidateAndExpert;
        }

        if (version.Expert?.IsCancelled == true)
        {
            return InterviewStatus.CancelledByExpert;
        }

        if (version.Candidate?.IsCancelled == true)
        {
            return InterviewStatus.CancelledByCandidate;
        }

        var now = DateTime.UtcNow;
        var candidateApproved = version.Candidate?.IsApproved ?? false;
        var expertApproved = version.Expert?.IsApproved ?? false;
        var bothApproved = candidateApproved && expertApproved;

        var isEnd = (version.EndUtc.HasValue && version.EndUtc.Value < now) || (!version.EndUtc.HasValue && version.StartUtc.AddHours(1) > now);

        if (bothApproved && isEnd)
        {
            return InterviewStatus.Completed;
        }

        var isInProcess = (version.StartUtc <= now && !version.EndUtc.HasValue) || (version.StartUtc <= now && version.EndUtc.HasValue && version.EndUtc.Value < now);

        if (bothApproved && isInProcess)
        {
            return InterviewStatus.InProgress;
        }

        if (bothApproved && string.IsNullOrEmpty(version.LinkToVideoCall))
        {
            return InterviewStatus.ConfirmedBoth;
        }

        if (bothApproved && !string.IsNullOrEmpty(version.LinkToVideoCall))
        {
            return InterviewStatus.ConfirmedBothLinkCreated;
        }

        if (candidateApproved)
        {
            return InterviewStatus.ConfirmedByCandidate;
        }

        if (expertApproved)
        {
            return InterviewStatus.ConfirmedByExpert;
        }

        if (!candidateApproved && !expertApproved)
        {
            return InterviewStatus.PendingConfirmation;
        }

        if (!candidateApproved && !expertApproved && isEnd)
        {
            return InterviewStatus.DidNotTakePlace;
        }

        if (!candidateApproved && expertApproved && isEnd)
        {
            return InterviewStatus.DidNotTakePlace;
        }

        if (candidateApproved && !expertApproved && isEnd)
        {
            return InterviewStatus.DidNotTakePlace;
        }

        return InterviewStatus.Unknown;
    }
}
