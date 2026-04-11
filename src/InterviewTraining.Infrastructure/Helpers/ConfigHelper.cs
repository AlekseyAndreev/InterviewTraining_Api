using Microsoft.Extensions.Configuration;
using System;

namespace InterviewTraining.Infrastructure.Helpers;

public static class ConfigHelper
{
    public static string GetSettingFromConfig(IConfiguration configuration, string firstName,
string secondName, bool throwIfEmpty = true)
    {
        return GetSettingFromConfig((IConfigurationRoot)configuration, firstName, secondName, throwIfEmpty);
    }

    public static string GetSettingFromConfig(IConfigurationRoot configurationRoot, string firstName, string secondName, bool throwIfEmpty = true)
    {
        var result = configurationRoot[$"{firstName}:{secondName}"];
        if (string.IsNullOrEmpty(result))
        {
            result = configurationRoot[$"{firstName}_{secondName}"];
        }

        if (string.IsNullOrEmpty(result) && throwIfEmpty)
        {
            throw new Exception($"Configuration setting does not exist. Setting name {firstName}:{secondName}");
        }

        return result;
    }
}
