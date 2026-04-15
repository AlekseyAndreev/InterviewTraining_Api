using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Exceptions;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Application.ManageAvailableTime.V10;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.Repositories.Interfaces;

namespace InterviewTraining.Infrastructure.Services;

/// <summary>
/// Сервис для работы с доступным временем пользователя
/// </summary>
public class UserAvailableTimeService : IUserAvailableTimeService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserAvailableTimeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Создать запись доступного времени
    /// </summary>
    public async Task<CreateAvailableTimeResponse> CreateAsync(
        CreateAvailableTimeRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (user == null)
        {
            throw new EntityNotFoundException("Пользователь не найден");
        }

        ValidateRequest(request);

        // Проверяем, если создаем AlwaysAvailable - не должно быть других активных записей этого типа
        if ((AvailabilityType)request.AvailabilityType == AvailabilityType.AlwaysAvailable)
        {
            var hasAlwaysAvailable = await _unitOfWork.UserAvailableTimes.HasAlwaysAvailableAsync(user.Id);
            if (hasAlwaysAvailable)
            {
                throw new BusinessLogicException("У пользователя уже есть активная запись с типом 'Доступен всегда'");
            }
        }

        var timeZoneId = user.TimeZoneId;
        TimeZoneInfo timeZoneInfo = null;
        
        if (timeZoneId.HasValue)
        {
            var timeZone = await _unitOfWork.TimeZones.GetByIdAsync(timeZoneId.Value);
            if (timeZone != null)
            {
                try
                {
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone.Code);
                }
                catch
                {
                    // Если timezone не найдена, используем UTC
                }
            }
        }

        TimeOnly? startTimeUtc = null;
        TimeOnly? endTimeUtc = null;

        if (request.StartTime.HasValue && timeZoneInfo != null)
        {
            startTimeUtc = ConvertTimeToUtc(request.StartTime.Value, timeZoneInfo, request.DayOfWeek, request.SpecificDate);
        }
        else if (request.StartTime.HasValue)
        {
            startTimeUtc = request.StartTime;
        }

        if (request.EndTime.HasValue && timeZoneInfo != null)
        {
            endTimeUtc = ConvertTimeToUtc(request.EndTime.Value, timeZoneInfo, request.DayOfWeek, request.SpecificDate);
        }
        else if (request.EndTime.HasValue)
        {
            endTimeUtc = request.EndTime;
        }

        // Создаем запись
        var availableTime = new UserAvailableTime
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            AvailabilityType = (AvailabilityType)request.AvailabilityType,
            DayOfWeek = request.DayOfWeek.HasValue ? (DayOfWeek)request.DayOfWeek.Value : null,
            SpecificDate = request.SpecificDate,
            StartTime = startTimeUtc,
            EndTime = endTimeUtc,
            CreatedUtc = DateTime.UtcNow,
            IsDeleted = false
        };

        await _unitOfWork.UserAvailableTimes.AddAsync(availableTime);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateAvailableTimeResponse
        {
            Id = availableTime.Id,
            Success = true
        };
    }

    /// <summary>
    /// Получить список доступного времени пользователя
    /// </summary>
    public async Task<GetAvailableTimeResponse> GetByCurrentUserAsync(
        string identityUserId,
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (user == null)
        {
            throw new EntityNotFoundException("Пользователь не найден");
        }

        var availableTimes = await _unitOfWork.UserAvailableTimes.GetActiveByUserIdAsync(user.Id);

        // Получаем timezone пользователя для отображения
        TimeZoneInfo timeZoneInfo = null;
        if (user.TimeZoneId.HasValue)
        {
            var timeZone = await _unitOfWork.TimeZones.GetByIdAsync(user.TimeZoneId.Value);
            if (timeZone != null)
            {
                try
                {
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone.Code);
                }
                catch
                {
                    // Если timezone не найдена, используем UTC
                }
            }
        }

        var result = new GetAvailableTimeResponse
        {
            AvailableTimes = availableTimes.Select(at => MapToDto(at, timeZoneInfo)).ToList()
        };

        return result;
    }

    /// <summary>
    /// Удалить запись доступного времени
    /// </summary>
    public async Task<DeleteAvailableTimeResponse> DeleteAsync(
        string identityUserId,
        Guid id,
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(identityUserId, cancellationToken);
        if (user == null)
        {
            throw new EntityNotFoundException("Пользователь не найден");
        }

        var availableTime = await _unitOfWork.UserAvailableTimes.GetByIdAsync(id);
        if (availableTime == null || availableTime.UserId != user.Id)
        {
            throw new EntityNotFoundException("Запись не найдена");
        }

        // Soft delete
        availableTime.IsDeleted = true;
        availableTime.ModifiedUtc = DateTime.UtcNow;
        _unitOfWork.UserAvailableTimes.Update(availableTime);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeleteAvailableTimeResponse
        {
            Success = true
        };
    }

    /// <summary>
    /// Обновить запись доступного времени
    /// </summary>
    public async Task<UpdateAvailableTimeResponse> UpdateAsync(
        UpdateAvailableTimeRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AdditionalUserInfos.GetByIdentityUserIdAsync(request.IdentityUserId, cancellationToken);
        if (user == null)
        {
            throw new EntityNotFoundException("Пользователь не найден");
        }

        var availableTime = await _unitOfWork.UserAvailableTimes.GetByIdAsync(request.AvailableTimeId);
        if (availableTime == null || availableTime.UserId != user.Id)
        {
            throw new EntityNotFoundException("Запись не найдена");
        }

        ValidateUpdateRequest(request);

        // Проверяем, если обновляем на AlwaysAvailable - не должно быть других активных записей этого типа
        if ((AvailabilityType)request.AvailabilityType == AvailabilityType.AlwaysAvailable)
        {
            var hasAlwaysAvailable = await _unitOfWork.UserAvailableTimes.HasAlwaysAvailableExcludingAsync(user.Id, request.AvailableTimeId);
            if (hasAlwaysAvailable)
            {
                throw new BusinessLogicException("У пользователя уже есть активная запись с типом 'Доступен всегда'");
            }
        }

        var timeZoneId = user.TimeZoneId;
        TimeZoneInfo timeZoneInfo = null;
        if (timeZoneId.HasValue)
        {
            var timeZone = await _unitOfWork.TimeZones.GetByIdAsync(timeZoneId.Value);
            if (timeZone != null)
            {
                try
                {
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone.Code);
                }
                catch
                {
                    // Если timezone не найдена, используем UTC
                }
            }
        }

        TimeOnly? startTimeUtc = null;
        TimeOnly? endTimeUtc = null;

        if (request.StartTime.HasValue && timeZoneInfo != null)
        {
            startTimeUtc = ConvertTimeToUtc(request.StartTime.Value, timeZoneInfo, request.DayOfWeek, request.SpecificDate);
        }
        else if (request.StartTime.HasValue)
        {
            startTimeUtc = request.StartTime;
        }

        if (request.EndTime.HasValue && timeZoneInfo != null)
        {
            endTimeUtc = ConvertTimeToUtc(request.EndTime.Value, timeZoneInfo, request.DayOfWeek, request.SpecificDate);
        }
        else if (request.EndTime.HasValue)
        {
            endTimeUtc = request.EndTime;
        }

        // Обновляем запись
        availableTime.AvailabilityType = (AvailabilityType)request.AvailabilityType;
        availableTime.DayOfWeek = request.DayOfWeek.HasValue ? (DayOfWeek)request.DayOfWeek.Value : null;
        availableTime.SpecificDate = request.SpecificDate;
        availableTime.StartTime = startTimeUtc;
        availableTime.EndTime = endTimeUtc;
        availableTime.ModifiedUtc = DateTime.UtcNow;

        _unitOfWork.UserAvailableTimes.Update(availableTime);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateAvailableTimeResponse
        {
            Id = availableTime.Id,
            Success = true
        };
    }

    /// <summary>
    /// Валидация запроса на создание
    /// </summary>
    private void ValidateRequest(CreateAvailableTimeRequest request)
    {
        var availabilityType = Convert(request.AvailabilityType);

        switch (availabilityType)
        {
            case AvailabilityType.AlwaysAvailable:
                break;

            case AvailabilityType.WeeklyFullDay:
                if (!request.DayOfWeek.HasValue)
                {
                    throw new BusinessLogicException("Для типа WeeklyFullDay необходимо указать день недели");
                }
                if (request.DayOfWeek.Value < 0 || request.DayOfWeek.Value > 6)
                {
                    throw new BusinessLogicException("День недели должен быть от 0 (воскресенье) до 6 (суббота)");
                }
                break;

            case AvailabilityType.WeeklyWithTime:
                if (!request.DayOfWeek.HasValue)
                {
                    throw new BusinessLogicException("Для типа WeeklyWithTime необходимо указать день недели");
                }
                if (!request.StartTime.HasValue)
                {
                    throw new BusinessLogicException("Для типа WeeklyWithTime необходимо указать время начала");
                }
                break;
            
             // TODO: подумать какие поля реально хотим передавать
            case AvailabilityType.SpecificDateTime:
                if (!request.SpecificDate.HasValue)
                {
                    throw new BusinessLogicException("Для типа SpecificDateTime необходимо указать конкретную дату");
                }
                if (!request.StartTime.HasValue)
                {
                    throw new BusinessLogicException("Для типа SpecificDateTime необходимо указать время начала");
                }
                break;

            default:
                throw new BusinessLogicException("Неверный тип доступности");
        }
    }

    /// <summary>
    /// Валидация запроса на обновление
    /// </summary>
    private void ValidateUpdateRequest(UpdateAvailableTimeRequest request)
    {
        var availabilityType = Convert(request.AvailabilityType);

        switch (availabilityType)
        {
            case AvailabilityType.AlwaysAvailable:
                break;

            case AvailabilityType.WeeklyFullDay:
                if (!request.DayOfWeek.HasValue)
                {
                    throw new BusinessLogicException("Для типа WeeklyFullDay необходимо указать день недели");
                }
                if (request.DayOfWeek.Value < 0 || request.DayOfWeek.Value > 6)
                {
                    throw new BusinessLogicException("День недели должен быть от 0 (воскресенье) до 6 (суббота)");
                }
                break;

            case AvailabilityType.WeeklyWithTime:
                if (!request.DayOfWeek.HasValue)
                {
                    throw new BusinessLogicException("Для типа WeeklyWithTime необходимо указать день недели");
                }
                if (!request.StartTime.HasValue)
                {
                    throw new BusinessLogicException("Для типа WeeklyWithTime необходимо указать время начала");
                }
                break;

            case AvailabilityType.SpecificDateTime:
                if (!request.SpecificDate.HasValue)
                {
                    throw new BusinessLogicException("Для типа SpecificDateTime необходимо указать конкретную дату");
                }
                if (!request.StartTime.HasValue)
                {
                    throw new BusinessLogicException("Для типа SpecificDateTime необходимо указать время начала");
                }
                break;

            default:
                throw new BusinessLogicException("Неверный тип доступности");
        }
    }

    /// <summary>
    /// Конвертация типа доступности
    /// </summary>
    private AvailabilityType Convert(ApplicationAvailabilityType availabilityType) =>
        availabilityType switch
        {
            ApplicationAvailabilityType.AlwaysAvailable => AvailabilityType.AlwaysAvailable,
            ApplicationAvailabilityType.WeeklyFullDay => AvailabilityType.WeeklyFullDay,
            ApplicationAvailabilityType.WeeklyWithTime => AvailabilityType.WeeklyWithTime,
            ApplicationAvailabilityType.SpecificDateTime => AvailabilityType.SpecificDateTime,
            _ => throw new BusinessLogicException("Не поддерживаемый формат для ApplicationAvailabilityType:" + availabilityType),
        };

    /// <summary>
    /// Конвертировать время из локального в UTC
    /// </summary>
    private static TimeOnly ConvertTimeToUtc(TimeOnly localTime, TimeZoneInfo timeZoneInfo, int? dayOfWeek, DateOnly? specificDate)
    {
        DateOnly dateToUse = specificDate ?? DateOnly.FromDateTime(DateTime.Today);
        var localDateTime = dateToUse.ToDateTime(localTime);
        
        var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZoneInfo);
        
        return TimeOnly.FromDateTime(utcDateTime);
    }

    /// <summary>
    /// Конвертировать время из UTC в локальное
    /// </summary>
    private static TimeOnly ConvertTimeFromUtc(TimeOnly utcTime, TimeZoneInfo timeZoneInfo, DateOnly? specificDate)
    {
        DateOnly dateToUse = specificDate ?? DateOnly.FromDateTime(DateTime.Today);
        
        var utcDateTime = dateToUse.ToDateTime(utcTime);
        var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZoneInfo);
        
        return TimeOnly.FromDateTime(localDateTime);
    }

    /// <summary>
    /// Маппинг в DTO
    /// </summary>
    private AvailableTimeDto MapToDto(UserAvailableTime entity, TimeZoneInfo timeZoneInfo)
    {
        TimeOnly? displayStartTime = entity.StartTime;
        TimeOnly? displayEndTime = entity.EndTime;

        // Конвертируем время обратно в timezone пользователя для отображения
        if (entity.StartTime.HasValue && timeZoneInfo != null)
        {
            displayStartTime = ConvertTimeFromUtc(entity.StartTime.Value, timeZoneInfo, entity.SpecificDate);
        }
        
        if (entity.EndTime.HasValue && timeZoneInfo != null)
        {
            displayEndTime = ConvertTimeFromUtc(entity.EndTime.Value, timeZoneInfo, entity.SpecificDate);
        }

        return new AvailableTimeDto
        {
            Id = entity.Id,
            AvailabilityType = (int)entity.AvailabilityType,
            DayOfWeek = entity.DayOfWeek.HasValue ? (int)entity.DayOfWeek.Value : null,
            SpecificDate = entity.SpecificDate,
            StartTime = displayStartTime,
            EndTime = displayEndTime,
            DisplayTime = GenerateDisplayTime(entity, displayStartTime, displayEndTime)
        };
    }

    /// <summary>
    /// Генерация текстового представления для отображения
    /// </summary>
    private string GenerateDisplayTime(UserAvailableTime entity, TimeOnly? startTime, TimeOnly? endTime)
    {
        var dayNames = new[] { "воскресенье", "понедельник", "вторник", "среда", "четверг", "пятница", "суббота" };

        return entity.AvailabilityType switch
        {
            AvailabilityType.AlwaysAvailable => "Доступен всегда",
            
            AvailabilityType.WeeklyFullDay => $"Каждый {dayNames[(int)entity.DayOfWeek.Value]} весь день",
            
            AvailabilityType.WeeklyWithTime when endTime.HasValue => 
                $"Каждый {dayNames[(int)entity.DayOfWeek.Value]} с {startTime:HH:mm} до {endTime:HH:mm}",
            
            AvailabilityType.WeeklyWithTime => 
                $"Каждый {dayNames[(int)entity.DayOfWeek.Value]} в {startTime:HH:mm}",
            
            AvailabilityType.SpecificDateTime when endTime.HasValue => 
                $"{entity.SpecificDate:dd.MM.yyyy} с {startTime:HH:mm} до {endTime:HH:mm}",
            
            AvailabilityType.SpecificDateTime => 
                $"{entity.SpecificDate:dd.MM.yyyy} в {startTime:HH:mm}",
            
            _ => "Неизвестный тип доступности"
        };
    }
}
