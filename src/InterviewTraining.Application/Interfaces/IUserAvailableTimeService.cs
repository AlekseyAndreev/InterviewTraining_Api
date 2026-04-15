using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.ManageAvailableTime.V10;

namespace InterviewTraining.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с доступным временем пользователя
/// </summary>
public interface IUserAvailableTimeService
{
    /// <summary>
    /// Создать запись доступного времени
    /// </summary>
    Task<CreateAvailableTimeResponse> CreateAsync(CreateAvailableTimeRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получить список доступного времени пользователя
    /// </summary>
    Task<GetAvailableTimeResponse> GetByCurrentUserAsync(string identityUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить запись доступного времени
    /// </summary>
    Task<DeleteAvailableTimeResponse> DeleteAsync(string identityUserId, Guid id, CancellationToken cancellationToken);
}
