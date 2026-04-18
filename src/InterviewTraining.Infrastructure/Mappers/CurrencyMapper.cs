using InterviewTraining.Application.GetAllCurrencies.V10;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Mappers;

public static class CurrencyMapper
{
    public static CurrencyResponse ToCurrencyResponse(this Currency entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new CurrencyResponse
        {
            Id = entity.Id,
            Code = entity.Code,
            NameEn = entity.NameEn,
            NameRu = entity.NameRu,
        };
    }
}
