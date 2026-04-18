using InterviewTraining.Application.GetAllCurrencies.V10;
using InterviewTraining.Application.Interfaces;
using InterviewTraining.Infrastructure.Mappers;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Services;

public class CurrencyService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : ICurrencyService
{
    private const string CacheKey = "AllCurrencies";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

    public async Task<CurrencyResponse[]> GetAllAsync(CancellationToken cancellationToken)
    {
        if (memoryCache.TryGetValue(CacheKey, out CurrencyResponse[] cachedCurrencies))
        {
            return cachedCurrencies;
        }

        var currencies = await unitOfWork.Currencies.GetAllAsync();
        var response = currencies.Select(x => x.ToCurrencyResponse()).ToArray();

        memoryCache.Set(CacheKey, response, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration,
            SlidingExpiration = TimeSpan.FromHours(5)
        });

        return response;
    }
}
