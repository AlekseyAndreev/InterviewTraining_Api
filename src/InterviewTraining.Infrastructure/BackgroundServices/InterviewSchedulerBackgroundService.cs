using System;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Infrastructure.BackgroundServices;

///<summary>
/// Фоновый сервис шедулера для обработки просроченных интервью
///</summary>
public class InterviewSchedulerBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<InterviewSchedulerBackgroundService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

    public InterviewSchedulerBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<InterviewSchedulerBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Interview Scheduler запущен. Периодичность: {Interval}", _interval);

        // Небольшая задержка при старте, чтобы дать приложению полностью инициализироваться
        await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var schedulerService = scope.ServiceProvider.GetRequiredService<IInterviewSchedulerService>();

                _logger.LogDebug("Запуск проверки просроченных интервью");
                await schedulerService.ProcessExpiredInterviewsAsync(stoppingToken);
                _logger.LogDebug("Проверка просроченных интервью завершена");
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                // Normal shutdown
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в работе Interview Scheduler");
            }

            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("Interview Scheduler остановлен");
    }
}
