using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

///<summary>
/// Интерфейс сервиса шедулера для обработки просроченных интервью
///</summary>
public interface IInterviewSchedulerService
{
    ///<summary>
    /// Обработать все просроченные интервью
    ///</summary>
    Task ProcessExpiredInterviewsAsync(CancellationToken cancellationToken);
}
