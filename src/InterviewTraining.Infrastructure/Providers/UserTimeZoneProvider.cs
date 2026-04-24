using InterviewTraining.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.Providers;

public class UserTimeZoneProvider(IUnitOfWork unitOfWork) : IUserTimeZoneProvider
{
    public async Task<string> GetTimeZoneCode(Guid? timeZoneId)
    {
        if (!timeZoneId.HasValue)
        {
            return null;
        }

        var timeZone = await unitOfWork.TimeZones.GetByIdAsync(timeZoneId.Value);
        return timeZone?.Code;
    }
}
