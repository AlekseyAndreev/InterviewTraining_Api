using InterviewTraining.Application.Common;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetMyInterviews.V10;
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
    public async Task<GetMyInterviewsResponse> GetMyInterviewsAsync(string identityUserId, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", identityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        var interviews = await _unitOfWork.Interviews.GetByUserIdAsync(currentUser.Id, cancellationToken);

        var timeZoneCode = await _userTimeZoneService.GetTimeZoneCode(currentUser.TimeZoneId);

        var result = new List<InterviewDto>();

        foreach (var interview in interviews)
        {
            var activeVersion = interview.ActiveInterviewVersion;
            var interviewDate = activeVersion != null
                ? DateTimeHelper.ConvertUtcToUserTimeZone(activeVersion.StartUtc, timeZoneCode)
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
