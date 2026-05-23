using InterviewTraining.Domain;
using System;

namespace InterviewTraining.Infrastructure.Helpers;

/// <summary>
/// Помощник для собеседований
/// </summary>
public static class InterviewHelper
{
    /// <summary>
    /// Вычисление статуса интервью на основе данных версии
    /// </summary>
    public static InterviewVersionState CalculateStatus(Interview interview, InterviewVersion version)
    {
        if (version == null)
        {
            return InterviewVersionState.Draft;
        }

        if (version.Candidate?.IsDeleted == true && version.Expert?.IsDeleted == true)
        {
            return InterviewVersionState.DeletedByCandidateAndExpert;
        }

        if (version.Expert?.IsDeleted == true)
        {
            return InterviewVersionState.DeletedByExpert;
        }

        if (version.Candidate?.IsDeleted == true)
        {
            return InterviewVersionState.DeletedByCandidate;
        }

        if (version.Candidate?.IsCancelled == true && version.Expert?.IsCancelled == true)
        {
            return InterviewVersionState.CancelledByCandidateAndExpert;
        }

        if (version.Expert?.IsCancelled == true)
        {
            return InterviewVersionState.CancelledByExpert;
        }

        if (version.Candidate?.IsCancelled == true)
        {
            return InterviewVersionState.CancelledByCandidate;
        }

        var nowUtc = DateTime.UtcNow;
        var candidateApproved = version.Candidate?.IsApproved ?? false;
        var expertApproved = version.Expert?.IsApproved ?? false;
        var bothApproved = candidateApproved && expertApproved;
        var isAdminApproved = version.IsAdminApproved;

        var isEnd = (version.EndUtc.HasValue && version.EndUtc.Value < nowUtc) || (!version.EndUtc.HasValue && version.StartUtc.AddHours(1) < nowUtc);

        if (bothApproved && isEnd && isAdminApproved)
        {
            return InterviewVersionState.Completed;
        }

        var isInProcess = (version.StartUtc <= nowUtc && !version.EndUtc.HasValue && version.StartUtc.AddHours(1) > nowUtc) || (version.StartUtc <= nowUtc && version.EndUtc.HasValue && version.EndUtc.Value > nowUtc);

        if (bothApproved && isInProcess && isAdminApproved)
        {
            return InterviewVersionState.InProgress;
        }

        var isStartDateExpired = version.StartUtc <= nowUtc;

        if (!candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredBothDidNotApprove;
        }

        if (candidateApproved && !expertApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredExpertDidNotApprove;
        }

        if (!candidateApproved && expertApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredCandidateDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && isStartDateExpired)
        {
            return InterviewVersionState.TimeExpiredBothApprovedAdminDidNotApprove;
        }

        if (bothApproved && !isAdminApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedBothAdminNotApproved;
        }

        if (bothApproved && isAdminApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedBothAdminApprovedTimeDidNotStart;
        }

        if (candidateApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedByCandidate;
        }

        if (expertApproved && !isStartDateExpired)
        {
            return InterviewVersionState.ConfirmedByExpert;
        }

        if (!candidateApproved && !expertApproved && !isStartDateExpired)
        {
            return InterviewVersionState.PendingConfirmation;
        }

        return InterviewVersionState.Unknown;
    }

    public static InterviewVersion CopyFrom(Guid interviewId, InterviewVersion activeVersion) =>
        new()
        {
            Id = Guid.NewGuid(),
            InterviewId = interviewId,
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
                IsCancelled = activeVersion.Candidate?.IsCancelled ?? false,
                CancelReason = activeVersion.Candidate?.CancelReason,
                IsDeleted = activeVersion.Candidate?.IsDeleted ?? false,
                IsRescheduled = activeVersion.Candidate?.IsRescheduled ?? false,
            },
            Expert = new ExpertInterviewData
            {
                IsApproved = activeVersion.Expert?.IsApproved ?? false,
                IsPaidToExpert = activeVersion.Expert?.IsPaidToExpert ?? false,
                IsCancelled = activeVersion.Expert?.IsCancelled ?? false,
                CancelReason = activeVersion.Expert?.CancelReason,
                IsDeleted = activeVersion.Expert?.IsDeleted ?? false,
                IsRescheduled = activeVersion.Expert?.IsRescheduled ?? false,
            }
        };
}
