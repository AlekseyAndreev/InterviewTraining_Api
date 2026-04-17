using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Базовая конфигурация для сущностей с UTC DateTime полями
/// </summary>
public static class UtcDateTimeConverter
{
    /// <summary>
    /// Конвертер для DateTime полей, который гарантирует DateTimeKind.Utc при чтении
    /// и сохраняет в БД в UTC
    /// </summary>
    public static readonly ValueConverter<DateTime, DateTime> DateTimeUtcConverter = new(
        convertToProviderExpression: dt => dt.Kind == DateTimeKind.Unspecified 
            ? DateTime.SpecifyKind(dt, DateTimeKind.Utc) 
            : dt.ToUniversalTime(),
        convertFromProviderExpression: dt => DateTime.SpecifyKind(dt, DateTimeKind.Utc));

    /// <summary>
    /// Конвертер для nullable DateTime полей
    /// </summary>
    public static readonly ValueConverter<DateTime?, DateTime?> NullableDateTimeUtcConverter = new(
        convertToProviderExpression: dt => dt.HasValue 
            ? (dt.Value.Kind == DateTimeKind.Unspecified 
                ? DateTime.SpecifyKind(dt.Value, DateTimeKind.Utc) 
                : dt.Value.ToUniversalTime()) 
            : null,
        convertFromProviderExpression: dt => dt.HasValue 
            ? DateTime.SpecifyKind(dt.Value, DateTimeKind.Utc) 
            : null);

    /// <summary>
    /// Применяет UTC конвертер к свойству DateTime
    /// </summary>
    public static PropertyBuilder<DateTime> HasUtcConversion(this PropertyBuilder<DateTime> builder)
    {
        return builder.HasConversion(DateTimeUtcConverter);
    }

    /// <summary>
    /// Применяет UTC конвертер к nullable свойству DateTime
    /// </summary>
    public static PropertyBuilder<DateTime?> HasUtcConversion(this PropertyBuilder<DateTime?> builder)
    {
        return builder.HasConversion(NullableDateTimeUtcConverter);
    }
}
