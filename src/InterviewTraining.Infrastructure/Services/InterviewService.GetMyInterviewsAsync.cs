using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Common;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetMyInterviews.V10;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с интервью
/// </summary>
public partial class InterviewService
{
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
            var activeVersion = interview.ActiveInterviewVersion;
            var interviewDate = activeVersion != null
                ? ConvertUtcToUserTimeZone(activeVersion.StartUtc, userTimeZone?.Code)
                : DateTime.MinValue;
            var status = CalculateStatus(interview, activeVersion);
            var dto = new InterviewDto
            {
                Id = interview.Id,
                ExpertId = interview.Expert?.IdentityUserId,
                ExpertName = interview.Expert?.FullName ?? "Не указан",
                CandidateId = interview.Candidate?.IdentityUserId,
                CandidateName = interview.Candidate?.FullName ?? "Не указан",
                Status = status,
                StatusDescriptionRu = InterviewStatusDescription.GetStatusDescriptionRu(status),
                StatusDescriptionEn = InterviewStatusDescription.GetStatusDescriptionEn(status),
                ScheduledAt = interviewDate,
            };
            
            result.Add(dto);
        }

        return new GetMyInterviewsResponse
        {
            Data = result
        };
    }
 }
