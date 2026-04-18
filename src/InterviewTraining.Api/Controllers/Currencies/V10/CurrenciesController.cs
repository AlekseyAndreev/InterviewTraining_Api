using InterviewTraining.Application.CustomMediatorLogic;
using InterviewTraining.Application.GetAllCurrencies.V10;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Api.Controllers.Currencies.V10;

[Route("api/v1/currencies")]
[ApiController]
public class CurrenciesController : BaseController<CurrenciesController>
{
    private readonly ICustomMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    ///<param name="mediator"></param>
    /// <param name="logger"></param>
    public CurrenciesController(ICustomMediator mediator,
        ILogger<CurrenciesController> logger
    )
        : base(logger)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить все валюты
    /// </summary>
    [HttpGet]
    [Authorize]
    public Task<CurrencyResponse[]> GetAllAsync(CancellationToken cancellationToken) =>
        _mediator.SendAsync(new GetAllCurrenciesRequest(), cancellationToken);
}
