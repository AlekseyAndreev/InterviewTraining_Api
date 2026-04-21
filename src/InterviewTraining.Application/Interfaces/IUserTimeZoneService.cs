using System;
using System.Threading.Tasks;

namespace InterviewTraining.Application.Interfaces;

public interface IUserTimeZoneService
{
    Task<string> GetTimeZoneCode(Guid? timeZoneId);
}
