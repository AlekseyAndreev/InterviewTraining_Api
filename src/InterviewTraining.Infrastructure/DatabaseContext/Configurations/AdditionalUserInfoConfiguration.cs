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
            .Property(x => x.PhotoLocal)
            .HasComment("Фото пользователя")
            .HasColumnName("photo_local")
            .IsRequired(false);

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
            .Property(x => x.IsCandidate)
            .HasComment("Признак кандидата")
            .HasColumnName("is_candidate")
            .IsRequired();

        builder
            .Property(x => x.IsExpert)
            .HasComment("Признак эксперта")
            .HasColumnName("is_expert")
            .IsRequired();

        builder
            .Property(x => x.TimeZoneId)
            .HasComment("Идентификатор часового пояса пользователя")
            .HasColumnName("time_zone_id")
            .IsRequired(false);

        builder
            .Property(x => x.InterviewPrice)
            .HasComment("Сумма оплаты за собеседование")
            .HasColumnName("interview_price")
            .HasPrecision(18, 2)
            .IsRequired(false);

        builder
            .Property(x => x.CurrencyId)
            .HasComment("Идентификатор валюты оплаты")
            .HasColumnName("currency_id")
            .IsRequired(false);

        builder
            .HasOne(x => x.TimeZone)
            .WithMany()
            .HasForeignKey(x => x.TimeZoneId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .HasConstraintName("fk_additional_user_info_currency")
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(x => x.MyRatingToUsers)
            .WithOne(x => x.UserFrom)
            .HasForeignKey(x => x.UserFromId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasMany(x => x.RatingFromUsers)
            .WithOne(x => x.UserTo)
            .HasForeignKey(x => x.UserToId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasMany(x => x.Skills)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.IdentityUserId)
            .IsUnique(true)
            .HasDatabaseName("ix_additional_user_info_identity_user_id");

        builder
            .HasIndex(x => x.IsExpert)
            .HasDatabaseName("ix_additional_user_info_is_expert");

        builder
            .HasIndex(x => x.IsCandidate)
            .HasDatabaseName("ix_additional_user_info_is_candidate");
    }
}
