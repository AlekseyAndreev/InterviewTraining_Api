using System;
using System.Collections.Generic;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    /// <summary>
    /// GUID валюты RUB
    /// </summary>
    public static Guid RubCurrencyGuid = Guid.Parse("30000000-0000-0000-0000-000000000001");

    /// <summary>
    /// GUID валюты USD
    /// </summary>
    public static Guid UsdCurrencyGuid = Guid.Parse("30000000-0000-0000-0000-000000000002");

    /// <summary>
    /// GUID валюты EUR
    /// </summary>
    public static Guid EurCurrencyGuid = Guid.Parse("30000000-0000-0000-0000-000000000003");

    /// <summary>
    /// Возвращает все валюты
    /// </summary>
    public static List<Domain.Currency> GetCurrencies()
    {
        var currencies = new List<Domain.Currency>
        {
            new()
            {
                Id = RubCurrencyGuid,
                Code = "RUB",
                NameRu = "Российский рубль",
                NameEn = "Russian Ruble",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = UsdCurrencyGuid,
                Code = "USD",
                NameRu = "Доллар США",
                NameEn = "US Dollar",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = EurCurrencyGuid,
                Code = "EUR",
                NameRu = "Евро",
                NameEn = "Euro",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000004"),
                Code = "GBP",
                NameRu = "Фунт стерлингов",
                NameEn = "British Pound Sterling",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000005"),
                Code = "CNY",
                NameRu = "Китайский юань",
                NameEn = "Chinese Yuan",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000006"),
                Code = "KZT",
                NameRu = "Казахстанский тенге",
                NameEn = "Kazakhstani Tenge",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000007"),
                Code = "BYN",
                NameRu = "Белорусский рубль",
                NameEn = "Belarusian Ruble",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000008"),
                Code = "UAH",
                NameRu = "Украинская гривна",
                NameEn = "Ukrainian Hryvnia",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000009"),
                Code = "PLN",
                NameRu = "Польский злотый",
                NameEn = "Polish Zloty",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000010"),
                Code = "CHF",
                NameRu = "Швейцарский франк",
                NameEn = "Swiss Franc",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000011"),
                Code = "JPY",
                NameRu = "Японская иена",
                NameEn = "Japanese Yen",
                CreatedUtc = _utc_date
            },
            new()
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000012"),
                Code = "TRY",
                NameRu = "Турецкая лира",
                NameEn = "Turkish Lira",
                CreatedUtc = _utc_date
            }
        };

        return currencies;
    }
}
