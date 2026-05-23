using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Providers;

public interface IUserTimeZoneProvider
{
    Task<string> GetTimeZoneCodeAsync(Guid? timeZoneId, CancellationToken cancellationToken);
}
