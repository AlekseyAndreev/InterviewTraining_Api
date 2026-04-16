using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса синхронизации пользователей
/// </summary>
public interface IUserSyncService
{
    /// <summary>
    /// Синхронизировать пользователя
    /// </summary>
    Task SyncUserAsync(string identityUserId, bool isCandidate, bool isExpert, CancellationToken cancellationToken);
}
