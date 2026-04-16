using System;
using System.Collections.Generic;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    public static Guid MoscowGuid = Guid.Parse("20000000-0000-0000-0000-000000000089");

    /// <summary>
    /// Возвращает все часовые пояса мира
    /// </summary>
    public static List<Domain.TimeZone> GetAllTimeZones()
    {
        var timeZones = new List<Domain.TimeZone>
        {
            // UTC
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000001"), Code = "UTC", Description = "Coordinated Universal Time", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC-12:00 to UTC-11:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000002"), Code = "Etc/GMT+12", Description = "UTC-12:00 (Baker Island)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000003"), Code = "Etc/GMT+11", Description = "UTC-11:00 (American Samoa)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000004"), Code = "Pacific/Niue", Description = "Niue Time (UTC-11:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC-10:00 to UTC-09:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000005"), Code = "Pacific/Honolulu", Description = "Hawaii-Aleutian Time (UTC-10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000006"), Code = "Pacific/Tahiti", Description = "Tahiti Time (UTC-10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000007"), Code = "Pacific/Marquesas", Description = "Marquesas Time (UTC-09:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000008"), Code = "America/Anchorage", Description = "Alaska Time (UTC-09:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000009"), Code = "Pacific/Gambier", Description = "Gambier Time (UTC-09:00)", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC-08:00 to UTC-07:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000010"), Code = "America/Los_Angeles", Description = "Pacific Time (UTC-08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000011"), Code = "America/Tijuana", Description = "Tijuana Time (UTC-08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000012"), Code = "Pacific/Pitcairn", Description = "Pitcairn Time (UTC-08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000013"), Code = "America/Denver", Description = "Mountain Time (UTC-07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000014"), Code = "America/Phoenix", Description = "Mountain Standard Time (UTC-07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000015"), Code = "America/Chihuahua", Description = "Chihuahua Time (UTC-07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000016"), Code = "America/Mazatlan", Description = "Mazatlan Time (UTC-07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC-06:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000017"), Code = "America/Chicago", Description = "Central Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000018"), Code = "America/Mexico_City", Description = "Mexico City Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000019"), Code = "America/Winnipeg", Description = "Winnipeg Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000020"), Code = "America/Regina", Description = "Saskatchewan Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000021"), Code = "America/Guatemala", Description = "Guatemala Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000022"), Code = "America/El_Salvador", Description = "El Salvador Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000023"), Code = "America/Managua", Description = "Managua Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000024"), Code = "America/Costa_Rica", Description = "Costa Rica Time (UTC-06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC-05:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000025"), Code = "America/New_York", Description = "Eastern Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000026"), Code = "America/Toronto", Description = "Toronto Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000027"), Code = "America/Montreal", Description = "Montreal Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000028"), Code = "America/Detroit", Description = "Detroit Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000029"), Code = "America/Bogota", Description = "Bogota Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000030"), Code = "America/Lima", Description = "Lima Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000031"), Code = "America/Panama", Description = "Panama Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000032"), Code = "America/Jamaica", Description = "Jamaica Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000033"), Code = "America/Havana", Description = "Cuba Time (UTC-05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC-04:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000034"), Code = "America/Caracas", Description = "Venezuela Time (UTC-04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000035"), Code = "America/Santiago", Description = "Chile Time (UTC-04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000036"), Code = "America/La_Paz", Description = "Bolivia Time (UTC-04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000037"), Code = "America/Santo_Domingo", Description = "Dominican Republic Time (UTC-04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000038"), Code = "America/Puerto_Rico", Description = "Puerto Rico Time (UTC-04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000039"), Code = "Atlantic/Bermuda", Description = "Bermuda Time (UTC-04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC-03:00 to UTC-02:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000040"), Code = "America/Sao_Paulo", Description = "Brasilia Time (UTC-03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000041"), Code = "America/Buenos_Aires", Description = "Argentina Time (UTC-03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000042"), Code = "America/Montevideo", Description = "Uruguay Time (UTC-03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000043"), Code = "America/Cayenne", Description = "French Guiana Time (UTC-03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000044"), Code = "America/Paramaribo", Description = "Suriname Time (UTC-03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000045"), Code = "America/Godthab", Description = "Greenland Time (UTC-03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000046"), Code = "Atlantic/South_Georgia", Description = "South Georgia Time (UTC-02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC-01:00 to UTC+01:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000047"), Code = "Atlantic/Azores", Description = "Azores Time (UTC-01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000048"), Code = "Atlantic/Cape_Verde", Description = "Cape Verde Time (UTC-01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000049"), Code = "Atlantic/Reykjavik", Description = "Iceland Time (UTC+00:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000050"), Code = "Europe/London", Description = "Greenwich Mean Time (UTC+00:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000051"), Code = "Europe/Dublin", Description = "Irish Time (UTC+00:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000052"), Code = "Europe/Lisbon", Description = "Portugal Time (UTC+00:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000053"), Code = "Africa/Casablanca", Description = "Morocco Time (UTC+00:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000054"), Code = "Africa/Monrovia", Description = "Liberia Time (UTC+00:00)", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC+01:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000055"), Code = "Europe/Berlin", Description = "Central European Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000056"), Code = "Europe/Paris", Description = "France Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000057"), Code = "Europe/Rome", Description = "Italy Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000058"), Code = "Europe/Madrid", Description = "Spain Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000059"), Code = "Europe/Amsterdam", Description = "Netherlands Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000060"), Code = "Europe/Brussels", Description = "Belgium Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000061"), Code = "Europe/Vienna", Description = "Austria Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000062"), Code = "Europe/Zurich", Description = "Switzerland Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000063"), Code = "Europe/Stockholm", Description = "Sweden Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000064"), Code = "Europe/Oslo", Description = "Norway Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000065"), Code = "Europe/Copenhagen", Description = "Denmark Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000066"), Code = "Europe/Warsaw", Description = "Poland Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000067"), Code = "Europe/Prague", Description = "Czech Republic Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000068"), Code = "Europe/Budapest", Description = "Hungary Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000069"), Code = "Europe/Belgrade", Description = "Serbia Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000070"), Code = "Africa/Lagos", Description = "West Africa Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000071"), Code = "Africa/Algiers", Description = "Algeria Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000072"), Code = "Africa/Tunis", Description = "Tunisia Time (UTC+01:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC+02:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000073"), Code = "Europe/Kiev", Description = "Ukraine Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000074"), Code = "Europe/Bucharest", Description = "Romania Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000075"), Code = "Europe/Sofia", Description = "Bulgaria Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000076"), Code = "Europe/Athens", Description = "Greece Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000077"), Code = "Europe/Helsinki", Description = "Finland Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000078"), Code = "Europe/Riga", Description = "Latvia Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000079"), Code = "Europe/Tallinn", Description = "Estonia Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000080"), Code = "Europe/Vilnius", Description = "Lithuania Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000081"), Code = "Europe/Chisinau", Description = "Moldova Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000082"), Code = "Africa/Cairo", Description = "Egypt Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000083"), Code = "Africa/Johannesburg", Description = "South Africa Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000084"), Code = "Africa/Harare", Description = "Zimbabwe Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000085"), Code = "Israel", Description = "Israel Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000086"), Code = "Asia/Beirut", Description = "Lebanon Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000087"), Code = "Asia/Damascus", Description = "Syria Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000088"), Code = "Asia/Amman", Description = "Jordan Time (UTC+02:00)", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC+03:00
            new() { Id = MoscowGuid, Code = "Europe/Moscow", Description = "Moscow Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000090"), Code = "Europe/Istanbul", Description = "Turkey Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000091"), Code = "Europe/Minsk", Description = "Belarus Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000092"), Code = "Asia/Baghdad", Description = "Iraq Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000093"), Code = "Asia/Riyadh", Description = "Saudi Arabia Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000094"), Code = "Asia/Kuwait", Description = "Kuwait Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000095"), Code = "Asia/Dubai", Description = "UAE Time (UTC+04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000096"), Code = "Asia/Bahrain", Description = "Bahrain Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000097"), Code = "Asia/Qatar", Description = "Qatar Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000098"), Code = "Africa/Nairobi", Description = "East Africa Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000099"), Code = "Africa/Addis_Ababa", Description = "Ethiopia Time (UTC+03:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC+04:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000100"), Code = "Asia/Tbilisi", Description = "Georgia Time (UTC+04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000101"), Code = "Asia/Yerevan", Description = "Armenia Time (UTC+04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000102"), Code = "Asia/Baku", Description = "Azerbaijan Time (UTC+04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000103"), Code = "Asia/Muscat", Description = "Oman Time (UTC+04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000104"), Code = "Indian/Mauritius", Description = "Mauritius Time (UTC+04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000105"), Code = "Indian/Reunion", Description = "Reunion Time (UTC+04:00)", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC+04:30 to UTC+05:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000106"), Code = "Asia/Tehran", Description = "Iran Time (UTC+03:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000107"), Code = "Asia/Kabul", Description = "Afghanistan Time (UTC+04:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000108"), Code = "Asia/Karachi", Description = "Pakistan Time (UTC+05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000109"), Code = "Asia/Tashkent", Description = "Uzbekistan Time (UTC+05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000110"), Code = "Asia/Ashgabat", Description = "Turkmenistan Time (UTC+05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000111"), Code = "Asia/Dushanbe", Description = "Tajikistan Time (UTC+05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000112"), Code = "Asia/Bishkek", Description = "Kyrgyzstan Time (UTC+06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000113"), Code = "Indian/Maldives", Description = "Maldives Time (UTC+05:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC+05:30 to UTC+06:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000114"), Code = "Asia/Kolkata", Description = "India Time (UTC+05:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000115"), Code = "Asia/Colombo", Description = "Sri Lanka Time (UTC+05:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000116"), Code = "Asia/Kathmandu", Description = "Nepal Time (UTC+05:45)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000117"), Code = "Asia/Dhaka", Description = "Bangladesh Time (UTC+06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000118"), Code = "Asia/Thimphu", Description = "Bhutan Time (UTC+06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000119"), Code = "Asia/Almaty", Description = "Almaty Time (UTC+06:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC+06:30 to UTC+07:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000120"), Code = "Asia/Yangon", Description = "Myanmar Time (UTC+06:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000121"), Code = "Indian/Cocos", Description = "Cocos Islands Time (UTC+06:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000122"), Code = "Asia/Bangkok", Description = "Thailand Time (UTC+07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000123"), Code = "Asia/Ho_Chi_Minh", Description = "Vietnam Time (UTC+07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000124"), Code = "Asia/Jakarta", Description = "Western Indonesia Time (UTC+07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000125"), Code = "Asia/Phnom_Penh", Description = "Cambodia Time (UTC+07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000126"), Code = "Asia/Vientiane", Description = "Laos Time (UTC+07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000127"), Code = "Indian/Christmas", Description = "Christmas Island Time (UTC+07:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC+08:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000128"), Code = "Asia/Shanghai", Description = "China Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000129"), Code = "Asia/Hong_Kong", Description = "Hong Kong Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000130"), Code = "Asia/Taipei", Description = "Taiwan Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000131"), Code = "Asia/Singapore", Description = "Singapore Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000132"), Code = "Asia/Kuala_Lumpur", Description = "Malaysia Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000133"), Code = "Asia/Manila", Description = "Philippines Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000134"), Code = "Asia/Makassar", Description = "Central Indonesia Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000135"), Code = "Australia/Perth", Description = "Western Australia Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000136"), Code = "Asia/Ulaanbaatar", Description = "Mongolia Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000137"), Code = "Asia/Brunei", Description = "Brunei Time (UTC+08:00)", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC+09:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000138"), Code = "Asia/Tokyo", Description = "Japan Time (UTC+09:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000139"), Code = "Asia/Seoul", Description = "South Korea Time (UTC+09:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000140"), Code = "Asia/Pyongyang", Description = "North Korea Time (UTC+09:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000141"), Code = "Asia/Jayapura", Description = "Eastern Indonesia Time (UTC+09:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000142"), Code = "Pacific/Palau", Description = "Palau Time (UTC+09:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC+09:30 to UTC+10:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000143"), Code = "Australia/Darwin", Description = "Central Australia Time (UTC+09:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000144"), Code = "Australia/Adelaide", Description = "South Australia Time (UTC+09:30)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000145"), Code = "Australia/Sydney", Description = "Eastern Australia Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000146"), Code = "Australia/Melbourne", Description = "Victoria Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000147"), Code = "Australia/Brisbane", Description = "Queensland Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000148"), Code = "Australia/Hobart", Description = "Tasmania Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000149"), Code = "Pacific/Port_Moresby", Description = "Papua New Guinea Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000150"), Code = "Pacific/Guam", Description = "Guam Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000151"), Code = "Pacific/Saipan", Description = "Northern Mariana Islands Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000152"), Code = "Asia/Vladivostok", Description = "Vladivostok Time (UTC+10:00)", CreatedUtc = _utc_date, IsDeleted = false },
            // UTC+11:00 to UTC+12:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000153"), Code = "Pacific/Noumea", Description = "New Caledonia Time (UTC+11:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000154"), Code = "Pacific/Efate", Description = "Vanuatu Time (UTC+11:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000155"), Code = "Pacific/Guadalcanal", Description = "Solomon Islands Time (UTC+11:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000156"), Code = "Pacific/Norfolk", Description = "Norfolk Island Time (UTC+11:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000157"), Code = "Pacific/Auckland", Description = "New Zealand Time (UTC+12:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000158"), Code = "Pacific/Fiji", Description = "Fiji Time (UTC+12:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000159"), Code = "Pacific/Funafuti", Description = "Tuvalu Time (UTC+12:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000160"), Code = "Pacific/Tarawa", Description = "Kiribati Time (UTC+12:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000161"), Code = "Pacific/Majuro", Description = "Marshall Islands Time (UTC+12:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000162"), Code = "Pacific/Nauru", Description = "Nauru Time (UTC+12:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000163"), Code = "Asia/Kamchatka", Description = "Kamchatka Time (UTC+12:00)", CreatedUtc = _utc_date, IsDeleted = false },
            
            // UTC+13:00 to UTC+14:00
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000164"), Code = "Pacific/Apia", Description = "Samoa Time (UTC+13:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000165"), Code = "Pacific/Tongatapu", Description = "Tonga Time (UTC+13:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000166"), Code = "Pacific/Enderbury", Description = "Phoenix Islands Time (UTC+13:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000167"), Code = "Pacific/Fakaofo", Description = "Tokelau Time (UTC+13:00)", CreatedUtc = _utc_date, IsDeleted = false },
            new() { Id = Guid.Parse("20000000-0000-0000-0000-000000000168"), Code = "Pacific/Kiritimati", Description = "Line Islands Time (UTC+14:00)", CreatedUtc = _utc_date, IsDeleted = false },
        };

        return timeZones;
    }
}
