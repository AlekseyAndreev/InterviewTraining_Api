using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности AdditionalUserInfo
/// </summary>
public class AdditionalUserInfoConfiguration : IEntityTypeConfiguration<Domain.AdditionalUserInfo>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.AdditionalUserInfo> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("additional_user_infos");
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
            .Property(x => x.IdentityUserId)
            .HasComment("Идентификатор пользователя в Identity")
            .HasColumnName("identity_user_id")
            .IsRequired()
            .HasMaxLength(450);

        builder
            .Property(x => x.FullName)
            .HasComment("Полное имя пользователя")
            .HasColumnName("full_name")
            .IsRequired(false)
            .HasMaxLength(500);

        builder
            .Property(x => x.Photo)
            .HasComment("URL фото пользователя")
            .HasColumnName("photo")
            .IsRequired(false)
            .HasMaxLength(2000);

        builder
            .Property(x => x.ShortDescription)
            .HasComment("Краткое описание")
            .HasColumnName("short_description")
            .IsRequired(false)
            .HasMaxLength(500);

        builder
            .Property(x => x.Description)
            .HasComment("Полное описание")
            .HasColumnName("description")
            .IsRequired(false)
            .HasMaxLength(4000);

        builder
            .Property(x => x.InterviewScheduleAtAnyTime)
            .HasComment("Готов проводить интервью в любое время")
            .HasColumnName("interview_schedule_at_any_time")
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .Property(x => x.IsCandidate)
            .HasComment("Признак кандидата")
            .HasColumnName("is_candidate")
            .IsRequired();

        builder
            .Property(x => x.IsExpert)
            .HasComment("Признак эксперта")
            .HasColumnName("is_expert")
            .IsRequired();

        // Связь: рейтинги, которые пользователь поставил другим
        builder
            .HasMany(x => x.MyRatingToUsers)
            .WithOne(x => x.UserFrom)
            .HasForeignKey(x => x.UserFromId)
            .OnDelete(DeleteBehavior.ClientCascade);

        // Связь: рейтинги, которые пользователю поставили другие
        builder
            .HasMany(x => x.RatingFromUsers)
            .WithOne(x => x.UserTo)
            .HasForeignKey(x => x.UserToId)
            .OnDelete(DeleteBehavior.ClientCascade);

        // Индекс для быстрого поиска по IdentityUserId
        builder
            .HasIndex(x => x.IdentityUserId)
            .HasDatabaseName("ix_additional_user_info_identity_user_id");

        // Индекс для поиска экспертов
        builder
            .HasIndex(x => x.IsExpert)
            .HasDatabaseName("ix_additional_user_info_is_expert");

        // Индекс для поиска кандидатов
        builder
            .HasIndex(x => x.IsCandidate)
            .HasDatabaseName("ix_additional_user_info_is_candidate");
    }
}
