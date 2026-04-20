using System;

namespace InterviewTraining.Infrastructure.Helpers;

public static class DateTimeHelper
{
    /// <summary>
    /// Название временной зоны для UTC
    /// </summary>
    private const string UtcName = "UTC";

    /// <summary>
    /// Конвертация UTC времени в часовой пояс пользователя
    /// </summary>
    public static DateTime? ConvertUtcToUserTimeZone(DateTime? utcTime, string timeZoneCode)
    {
        if (!utcTime.HasValue)
        {
            return null;
        }

        return ConvertUtcToUserTimeZone(utcTime.Value, timeZoneCode);
    }

    /// <summary>
    /// Конвертация UTC времени в часовой пояс пользователя
    /// </summary>
    public static DateTime ConvertUtcToUserTimeZone(DateTime utcTime, string timeZoneCode)
    {
        if (string.IsNullOrEmpty(timeZoneCode) || timeZoneCode.Equals(UtcName, StringComparison.OrdinalIgnoreCase))
        {
            return utcTime;
        }

        try
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);
        }
        catch (TimeZoneNotFoundException)
        {
            return utcTime;
        }
        catch (InvalidTimeZoneException)
        {
            return utcTime;
        }
    }
}
