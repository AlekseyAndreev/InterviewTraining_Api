using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.CustomMediatorLogic;

/// <summary>
/// Интерфейс обработчика запроса для <see cref="ICustomMediator"/>
/// </summary>
///<typeparam name="TRequest">Тип запроса</typeparam>
/// <typeparam name="TResponse">Тип ответа</typeparam>
public interface IMediatorHandler<TRequest, TResponse> where TRequest : IMediatorRequest<TResponse>
{
    /// <summary>
    /// Обработать запрос
    /// </summary>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
