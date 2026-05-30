using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetAllInterviewsForAdmin.V10;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
    public async Task<GetAllInterviewsForAdminResponse> GetAllInterviewsForAdminAsync(string identityUserId, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", identityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        if (!currentUser.IsAdmin)
        {
            _logger.LogWarning("Пользователь {UserId} не является администратором", identityUserId);
            throw new BusinessLogicException("Доступно только для администраторов");
        }

        var interviews = await _unitOfWork.Interviews.GetAllForAdminAsync(cancellationToken);

        var result = new List<InterviewForAdminDto>();

        foreach (var interview in interviews)
        {
            var activeVersion = interview.ActiveInterviewVersion;
            var isCandidateDeleted = interview.Candidate?.IsDeleted ?? true;
            var isExpertDeleted = interview.Expert?.IsDeleted ?? true;
            var calculatedStatus = activeVersion != null 
                ? InterviewHelper.CalculateStatus(interview, activeVersion) 
                : InterviewVersionState.Unknown;

            var dto = new InterviewForAdminDto
            {
                Id = interview.Id,
                ExpertId = interview.Expert?.IdentityUserId,
                ExpertName = interview.Expert?.FullName ?? (isExpertDeleted ? "[Удалён]" : "Не указан"),
                CandidateId = interview.Candidate?.IdentityUserId,
                CandidateName = interview.Candidate?.FullName ?? (isCandidateDeleted ? "[Удалён]" : "Не указан"),
                Status = calculatedStatus.ToString(),
                StatusDescriptionRu = InterviewStatusDescriptionHelper.GetStatusDescriptionRu(calculatedStatus),
                StatusDescriptionEn = InterviewStatusDescriptionHelper.GetStatusDescriptionEn(calculatedStatus),
                ScheduledAtUtc = activeVersion?.StartUtc ?? DateTime.MinValue,
                IsDeleted = isCandidateDeleted || isExpertDeleted,
                IsCancelledByCandidate = activeVersion?.Candidate?.IsCancelled ?? false,
                IsCancelledByExpert = activeVersion?.Expert?.IsCancelled ?? false,
                IsConfirmedByCandidate = activeVersion?.Candidate?.IsApproved ?? false,
                IsConfirmedByExpert = activeVersion?.Expert?.IsApproved ?? false,
                CreatedAt = interview.CreatedUtc,
            };
            result.Add(dto);
        }

        return new GetAllInterviewsForAdminResponse
        {
            Data = result
        };
    }
}
