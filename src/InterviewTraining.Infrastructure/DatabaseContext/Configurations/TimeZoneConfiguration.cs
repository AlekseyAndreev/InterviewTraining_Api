using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности TimeZone
/// </summary>
public class TimeZoneConfiguration : IEntityTypeConfiguration<Domain.TimeZone>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.TimeZone> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("time_zones");
                t.Metadata.SetSchema(null);
            });

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasComment("Уникальный идентификатор")
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(x => x.CreatedUtc)
            .HasComment("Дата и время создания записи в таблице")
            .HasColumnName("created_utc")
            .IsRequired();

        builder
            .Property(x => x.ModifiedUtc)
            .HasComment("Дата и время последнего изменения записи в таблице")
            .HasColumnName("modified_utc")
            .IsRequired(false);

        builder
            .Property(x => x.IsDeleted)
            .HasComment("Признак удалена запись или нет")
            .HasColumnName("is_deleted")
            .IsRequired();

        builder
            .Property(x => x.Code)
            .HasComment("Код часового пояса")
            .HasColumnName("code")
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Description)
            .HasComment("Наименование часового пояса")
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(500);

        // Индекс для быстрого поиска по коду
        builder
            .HasIndex(x => x.Code)
            .IsUnique(true)
            .HasDatabaseName("ix_time_zones_code");
    }
}
