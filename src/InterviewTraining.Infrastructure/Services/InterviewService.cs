using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.CancelInterview.V10;
using InterviewTraining.Application.ConfirmInterview.V10;
using InterviewTraining.Application.CreateInterview.V10;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.GetInterviewInfo.V10;
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
                ExpertId = interview.Expert?.IdentityUserId,
                ExpertName = interview.Expert?.FullName ?? "Не указан",
                CandidateId = interview.Candidate?.IdentityUserId,
                CandidateName = interview.Candidate?.FullName ?? "Не указан",
                Status = CalculateStatus(interview.ActiveInterviewVersion),
                StatusDescription = "StatusDescription",
                ScheduledAt = interviewDate,
                Notes = interview.ActiveInterviewVersion?.Candidate?.Notes
            };
            
            result.Add(dto);
        }

        return new GetMyInterviewsResponse
        {
            Data = result
        };
    }

    /// <summary>
    /// Вычисление статуса интервью на основе данных версии
    /// </summary>
    private static string CalculateStatus(InterviewVersion version)
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
        if (request.ExpertId == request.CandidateId)
        {
            throw new BusinessLogicException("Эксперт и кандидат один и тот же пользователь");
        }

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

        Guid? interviewLanguageId = null;
        if (request.InterviewLanguageId.HasValue)
        {
            var existsLang = await _unitOfWork.InterviewLanguages.AnyAsync(x => x.Id == request.InterviewLanguageId.Value && !x.IsDeleted);
            if (!existsLang)
            {
                throw new BusinessLogicException("Язык собеседования не найден в БД");
            }
            else
            {
                interviewLanguageId = request.InterviewLanguageId.Value;
            }
        }

        var startUtc = ConvertUserTimeToUtc(request.Date, request.Time, candidate.TimeZone?.Code);

        var interview = new Interview
        {
            Id = Guid.NewGuid(),
            CandidateId = candidate.Id,
            ExpertId = expert.Id,
            ActiveInterviewVersionId = null,
            CreatedUtc = DateTime.UtcNow
        };

        await _unitOfWork.Interviews.AddAsync(interview);
        await _unitOfWork.SaveChangesAsync();

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
            CreatedUtc = DateTime.UtcNow,
            LanguageId = interviewLanguageId,
        };

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
    /// Получить детальную информацию по собеседованию
    /// </summary>
    public async Task<GetInterviewInfoResponse> GetInterviewInfoAsync(GetInterviewInfoRequest request, CancellationToken cancellationToken)
    {
        // Получаем текущего пользователя
        var currentUser = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (currentUser == null)
        {
            _logger.LogWarning("Не найдена информация по пользователю {UserId}", request.IdentityUserId);
            throw new BusinessLogicException("Не найдена информация по пользователю");
        }

        // Получаем интервью с деталями
        var interview = await _unitOfWork.Interviews.GetWithDetailsAsync(request.InterviewId, cancellationToken);
        if (interview == null)
        {
            _logger.LogWarning("Интервью с идентификатором {InterviewId} не найдено", request.InterviewId);
            throw new EntityNotFoundException("Собеседование не найдено");
        }

        // Проверка прав доступа
        var hasAccess = request.IsAdmin
            || interview.CandidateId == currentUser.Id
            || interview.ExpertId == currentUser.Id;

        if (!hasAccess)
        {
            _logger.LogWarning("Пользователь {UserId} не имеет доступа к интервью {InterviewId}",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("У вас нет доступа к этому собеседованию");
        }

        // Получаем активную версию интервью
        var activeVersion = interview.ActiveInterviewVersion;

        if (activeVersion == null)
        {
            throw new BusinessLogicException("Не задана активная версия для интервью");
        }

        // Определяем часовой пояс пользователя для конвертации времени
        var userTimeZone = currentUser.TimeZoneId.HasValue
            ? await _unitOfWork.TimeZones.GetByIdAsync(currentUser.TimeZoneId.Value)
            : null;
        var timeZoneCode = userTimeZone?.Code;

        // Формируем ответ
        var response = new GetInterviewInfoResponse
        {
            Id = interview.Id,
            Status = CalculateStatus(activeVersion),
            StartDateTime = ConvertUtcToUserTimeZone(activeVersion.StartUtc, timeZoneCode),
            EndDateTime = activeVersion.EndUtc.HasValue == true
                ? ConvertUtcToUserTimeZone(activeVersion.EndUtc.Value, timeZoneCode)
                : null,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            Notes = activeVersion.Candidate.Notes,
            CreatedUtc = ConvertUtcToUserTimeZone(interview.CreatedUtc, timeZoneCode),
            Candidate = MapParticipant(interview.Candidate),
            Expert = MapParticipant(interview.Expert),
            Language = activeVersion.Language != null ? MapLanguage(activeVersion.Language) : null,
            CandidateApproval = activeVersion.Candidate != null
                ? MapApproval(activeVersion.Candidate)
                : null,
            ExpertApproval = activeVersion.Expert != null
                ? MapApproval(activeVersion.Expert)
                : null
        };

        return response;
    }

    /// <summary>
    /// Маппинг участника интервью
    /// </summary>
    private static InterviewParticipantDto MapParticipant(AdditionalUserInfo user)
    {
        if (user == null) return null;

        return new InterviewParticipantDto
        {
            Id = user.Id,
            IdentityUserId = user.IdentityUserId,
            FullName = user.FullName ?? "Не указан",
            Photo = user.PhotoLocal,
            ShortDescription = user.ShortDescription
        };
    }

    /// <summary>
    /// Маппинг языка интервью
    /// </summary>
    private static InterviewLanguageDto MapLanguage(InterviewLanguage language)
    {
        if (language == null) return null;

        return new InterviewLanguageDto
        {
            Id = language.Id,
            Code = language.Code,
            NameRu = language.NameRu,
            NameEn = language.NameEn
        };
    }

    /// <summary>
    /// Маппинг данных подтверждения
    /// </summary>
    private static ParticipantApprovalDto MapApproval(BaseUserInterviewData data)
    {
        if (data == null)
        {
            return null;
        }

        return new ParticipantApprovalDto
        {
            IsApproved = data.IsApproved,
            IsCancelled = data.IsCancelled,
            CancelReason = data.CancellReason
        };
    }

    /// <summary>
    /// Отменить собеседование
    /// </summary>
    public async Task<CancelInterviewResponse> CancelInterviewAsync(CancelInterviewRequest request, CancellationToken cancellationToken)
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

        if (isCandidate && activeVersion.Expert?.IsCancelled == true)
        {
            _logger.LogWarning("Кандидат {UserId} пытается отменить интервью {InterviewId}, которое уже отменено экспертом",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("Собеседование уже отменено экспертом. Вы не можете его отменить.");
        }

        if (isExpert && activeVersion.Candidate?.IsCancelled == true)
        {
            _logger.LogWarning("Эксперт {UserId} пытается отменить интервью {InterviewId}, которое уже отменено кандидатом",
                currentUser.Id, interview.Id);
            throw new BusinessLogicException("Собеседование уже отменено кандидатом. Вы не можете его отменить.");
        }

        if (isCandidate && activeVersion.Candidate?.IsCancelled == true)
        {
            throw new BusinessLogicException("Вы уже отменили это собеседование");
        }

        if (isExpert && activeVersion.Expert?.IsCancelled == true)
        {
            throw new BusinessLogicException("Вы уже отменили это собеседование");
        }

        var newVersion = new InterviewVersion
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            StartUtc = activeVersion.StartUtc,
            EndUtc = activeVersion.EndUtc,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            LanguageId = activeVersion.LanguageId,
            CreatedUtc = DateTime.UtcNow,
            Candidate = new CandidateInterviewData
            {
                IsApproved = activeVersion.Candidate?.IsApproved ?? false,
                IsPaid = activeVersion.Candidate?.IsPaid ?? false,
                IsCancelled = isCandidate ? true : (activeVersion.Candidate?.IsCancelled ?? false),
                CancellReason = isCandidate ? request.CancelReason : activeVersion.Candidate?.CancellReason,
                Notes = activeVersion.Candidate?.Notes
            },
            Expert = new BaseUserInterviewData
            {
                IsApproved = activeVersion.Expert?.IsApproved ?? false,
                IsPaid = activeVersion.Expert?.IsPaid ?? false,
                IsCancelled = isExpert ? true : (activeVersion.Expert?.IsCancelled ?? false),
                CancellReason = isExpert ? request.CancelReason : activeVersion.Expert?.CancellReason
            }
        };

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        // Делаем новую версию активной
        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        var cancelReasonText = string.IsNullOrEmpty(request.CancelReason) ? "без указания причины" : $"по причине: {request.CancelReason}";
        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Собеседование {InterviewId} отменено {Role} {UserId} {Reason}",
            interview.Id, userRole, currentUser.Id, cancelReasonText);

        return new CancelInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }

    /// <summary>
    /// Подтвердить собеседование
    /// </summary>
    public async Task<ConfirmInterviewResponse> ConfirmInterviewAsync(ConfirmInterviewRequest request, CancellationToken cancellationToken)
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

        // Проверяем, не отменено ли собеседование
        if (activeVersion.Candidate?.IsCancelled == true || activeVersion.Expert?.IsCancelled == true)
        {
            _logger.LogWarning("Попытка подтвердить отменённое интервью {InterviewId}", interview.Id);
            throw new BusinessLogicException("Невозможно подтвердить отменённое собеседование");
        }

        // Проверяем, не подтверждено ли уже пользователем
        if (isCandidate && activeVersion.Candidate?.IsApproved == true)
        {
            throw new BusinessLogicException("Вы уже подтвердили это собеседование");
        }

        if (isExpert && activeVersion.Expert?.IsApproved == true)
        {
            throw new BusinessLogicException("Вы уже подтвердили это собеседование");
        }

        // Создаём новую версию с подтверждением
        var newVersion = new InterviewVersion
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            StartUtc = activeVersion.StartUtc,
            EndUtc = activeVersion.EndUtc,
            LinkToVideoCall = activeVersion.LinkToVideoCall,
            LanguageId = activeVersion.LanguageId,
            CreatedUtc = DateTime.UtcNow,
            Candidate = new CandidateInterviewData
            {
                IsApproved = isCandidate ? true : (activeVersion.Candidate?.IsApproved ?? false),
                IsPaid = activeVersion.Candidate?.IsPaid ?? false,
                IsCancelled = activeVersion.Candidate?.IsCancelled ?? false,
                CancellReason = activeVersion.Candidate?.CancellReason,
                Notes = activeVersion.Candidate?.Notes
            },
            Expert = new BaseUserInterviewData
            {
                IsApproved = isExpert ? true : (activeVersion.Expert?.IsApproved ?? false),
                IsPaid = activeVersion.Expert?.IsPaid ?? false,
                IsCancelled = activeVersion.Expert?.IsCancelled ?? false,
                CancellReason = activeVersion.Expert?.CancellReason
            }
        };

        await _unitOfWork.InterviewVersions.AddAsync(newVersion);

        // Делаем новую версию активной
        interview.ActiveInterviewVersionId = newVersion.Id;
        _unitOfWork.Interviews.Update(interview);

        await _unitOfWork.SaveChangesAsync();

        var userRole = isCandidate ? "кандидатом" : "экспертом";
        _logger.LogInformation("Собеседование {InterviewId} подтверждено {Role} {UserId}",
            interview.Id, userRole, currentUser.Id);

        return new ConfirmInterviewResponse
        {
            InterviewId = interview.Id,
            NewVersionId = newVersion.Id,
            Success = true,
        };
    }
 }
