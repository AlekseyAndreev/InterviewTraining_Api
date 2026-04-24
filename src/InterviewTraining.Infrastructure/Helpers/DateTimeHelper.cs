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

    /// <summary>
    /// Конвертация времени пользователя в UTC
    /// </summary>
    public static DateTime ConvertUserTimeToUtc(DateOnly date, TimeOnly time, string timeZoneCode)
    {
        var localDateTime = date.ToDateTime(time);

        if (string.IsNullOrEmpty(timeZoneCode) || timeZoneCode.Equals("UTC", StringComparison.OrdinalIgnoreCase))
        {
            return localDateTime;
        }

        try
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZoneInfo);
        }
        catch (TimeZoneNotFoundException)
        {
            // Если часовой пояс не найден, считаем что время уже в UTC
            return localDateTime;
        }
        catch (InvalidTimeZoneException)
        {
            return localDateTime;
        }
    }
}
