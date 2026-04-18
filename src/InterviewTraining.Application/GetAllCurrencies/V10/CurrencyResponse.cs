using System;

namespace InterviewTraining.Application.GetAllCurrencies.V10;

public class CurrencyResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }
}
