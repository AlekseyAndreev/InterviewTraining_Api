using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.CustomMediatorLogic;

/// <summary>
/// Реализация паттерна Mediator для CQRS
/// </summary>
public class CustomMediator(IServiceProvider serviceProvider) : ICustomMediator
{
    public Task<TResponse> SendAsync<TResponse>(IMediatorRequest<TResponse> request, CancellationToken cancellationToken)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IMediatorHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException(
                $"Handler not found for request type {requestType.Name}. " +
                $"Make sure IMediatorHandler<{requestType.Name}, {typeof(TResponse).Name}> is registered in DI container.");
        }
        
        var handleMethod = handlerType.GetMethod("HandleAsync");

        if (handleMethod == null)
        {
            throw new InvalidOperationException(
                $"Method HandleAsync not found on handler type {handlerType.Name}");
        }
        
        var result = handleMethod.Invoke(handler, new object[] { request, cancellationToken });

        return (Task<TResponse>)result;
    }
}
