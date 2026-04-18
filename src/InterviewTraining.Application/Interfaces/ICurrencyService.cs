using InterviewTraining.Application.GetAllCurrencies.V10;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface ICurrencyService
{
    Task<CurrencyResponse[]> GetAllAsync(CancellationToken cancellationToken);
}
