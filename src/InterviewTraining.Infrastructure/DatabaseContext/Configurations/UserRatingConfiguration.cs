using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности UserRating
/// </summary>
public class UserRatingConfiguration : IEntityTypeConfiguration<Domain.UserRating>
{
    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.UserRating> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("user_ratings");
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
            .Property(x => x.UserFromId)
            .HasComment("Идентификатор пользователя, который поставил рейтинг")
            .HasColumnName("user_from_id")
            .IsRequired();

        builder
            .Property(x => x.UserToId)
            .HasComment("Идентификатор пользователя, которому поставили рейтинг")
            .HasColumnName("user_to_id")
            .IsRequired();

        builder
            .Property(x => x.RatingValue)
            .HasComment("Значение рейтинга от 1 до 5")
            .HasColumnName("rating_value")
            .IsRequired();

        builder
            .Property(x => x.Comment)
            .HasComment("Комментарий к рейтингу")
            .HasColumnName("comment")
            .IsRequired(false)
            .HasMaxLength(2000);

        // Составной индекс для проверки уникальности рейтинга (один пользователь может поставить рейтинг другому только один раз)
        builder
            .HasIndex(x => new { x.UserFromId, x.UserToId })
            .HasDatabaseName("ix_user_rating_user_from_to")
            .IsUnique();

        // Индекс для быстрого получения рейтингов пользователя
        builder
            .HasIndex(x => x.UserToId)
            .HasDatabaseName("ix_user_rating_user_to_id");
    }
}
