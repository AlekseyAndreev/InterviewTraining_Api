using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.CustomMediatorLogic;

/// <summary>
/// ICustomMediator
/// </summary>
public interface ICustomMediator
{
    /// <summary>
    /// Отправить запрос и получить ответ
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse> SendAsync<TResponse>(IMediatorRequest<TResponse> request, CancellationToken cancellationToken);
}
