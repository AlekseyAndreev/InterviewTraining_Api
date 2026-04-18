using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.GetAllCurrencies.V10;

public class GetAllCurrenciesHandler(ICurrencyService service) : IMediatorHandler<GetAllCurrenciesRequest, CurrencyResponse[]>
{
    public async Task<CurrencyResponse[]> HandleAsync(GetAllCurrenciesRequest request, CancellationToken cancellationToken)
    {
        return await service.GetAllAsync(cancellationToken);
    }
}
