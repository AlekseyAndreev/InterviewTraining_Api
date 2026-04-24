using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Providers;

public interface IUserTimeZoneProvider
{
    Task<string> GetTimeZoneCode(Guid? timeZoneId);
}
