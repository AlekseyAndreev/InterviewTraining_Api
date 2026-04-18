using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности Currency
/// </summary>
public class CurrencyConfiguration : IEntityTypeConfiguration<Domain.Currency>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.Currency> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("currencies");
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
            .Property(x => x.Code)
            .HasComment("Код валюты (ISO 4217)")
            .HasColumnName("code")
            .IsRequired()
            .HasMaxLength(3);

        builder
            .Property(x => x.NameRu)
            .HasComment("Название валюты на русском языке")
            .HasColumnName("name_ru")
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.NameEn)
            .HasComment("Название валюты на английском языке")
            .HasColumnName("name_en")
            .IsRequired()
            .HasMaxLength(100);

        // Индекс для быстрого поиска по коду
        builder
            .HasIndex(x => x.Code)
            .IsUnique(true)
            .HasDatabaseName("ix_currencies_code");
    }
}
