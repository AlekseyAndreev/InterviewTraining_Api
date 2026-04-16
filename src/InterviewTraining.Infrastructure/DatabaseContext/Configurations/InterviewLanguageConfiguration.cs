using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности InterviewLanguage
/// </summary>
public class InterviewLanguageConfiguration : IEntityTypeConfiguration<Domain.InterviewLanguage>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.InterviewLanguage> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("interview_languages");
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
            .HasComment("Код")
            .HasColumnName("code")
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(x => x.NameRu)
            .HasComment("Наименование языка по русски")
            .HasColumnName("name_ru")
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(x => x.NameEn)
            .HasComment("Наименование языка по английски")
            .HasColumnName("name_en")
            .IsRequired()
            .HasMaxLength(255);
    }
}
