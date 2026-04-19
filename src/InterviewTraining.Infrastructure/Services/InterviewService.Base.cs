using InterviewTraining.Application.Common;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.SignalR;
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
    private readonly IInterviewNotificationService _notificationService;

    public InterviewService(
        IUnitOfWork unitOfWork,
        ILogger<InterviewService> logger,
        IInterviewNotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Конвертация UTC времени в часовой пояс пользователя
    /// </summary>
    private static DateTime? ConvertUtcToUserTimeZone(DateTime? utcTime, string timeZoneCode)
    {
        if (!utcTime.HasValue)
        {
            return null;
        }

        return ConvertUtcToUserTimeZone(utcTime.Value, timeZoneCode);
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

        if (version.Candidate?.IsDeleted == true && version.Expert?.IsDeleted == true)
        {
            return InterviewStatus.DeletedByCandidateAndExpert;
        }

        if (version.Expert?.IsDeleted == true)
        {
            return InterviewStatus.DeletedByExpert;
        }

        if (version.Candidate?.IsDeleted == true)
        {
            return InterviewStatus.DeletedByCandidate;
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

        var nowUtc = DateTime.UtcNow;
        var candidateApproved = version.Candidate?.IsApproved ?? false;
        var expertApproved = version.Expert?.IsApproved ?? false;
        var bothApproved = candidateApproved && expertApproved;
        var isAdminApproved = version.IsAdminApproved;

        var isEnd = (version.EndUtc.HasValue && version.EndUtc.Value < nowUtc) || (!version.EndUtc.HasValue && version.StartUtc.AddHours(1) < nowUtc);

        if (bothApproved && isEnd && isAdminApproved)
        {
            return InterviewStatus.Completed;
        }

        var isInProcess = (version.StartUtc <= nowUtc && !version.EndUtc.HasValue && version.StartUtc.AddHours(1) > nowUtc) || (version.StartUtc <= nowUtc && version.EndUtc.HasValue && version.EndUtc.Value > nowUtc);

        if (bothApproved && isInProcess && isAdminApproved)
        {
            return InterviewStatus.InProgress;
        }

        var isStartDateExpired = version.StartUtc <= nowUtc;

        if (!candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredBothDidNotApprove;
        }

        if (candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredExpertDidNotApprove;
        }

        if (!candidateApproved && expertApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredCandidateDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && isStartDateExpired)
        {
            return InterviewStatus.TimeExpiredBothApprovedAdminDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedBothAdminNotApproved;
        }

        if (bothApproved && isAdminApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedBothAdminApprovedTimeDidNotStart;
        }

        if (candidateApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedByCandidate;
        }

        if (expertApproved && !isStartDateExpired)
        {
            return InterviewStatus.ConfirmedByExpert;
        }

        if (!candidateApproved && !expertApproved && !isStartDateExpired)
        {
            return InterviewStatus.PendingConfirmation;
        }

        return InterviewStatus.Unknown;
    }
}
