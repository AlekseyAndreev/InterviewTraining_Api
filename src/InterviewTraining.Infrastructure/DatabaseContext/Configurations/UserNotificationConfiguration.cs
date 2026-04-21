using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

public class UserNotificationConfiguration : IEntityTypeConfiguration<Domain.UserNotification>
{
    public void Configure(EntityTypeBuilder<Domain.UserNotification> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("user_notifications");
                t.Metadata.SetSchema(null);
            });

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasComment("Уникальный идентификатор уведомления")
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(x => x.CreatedUtc)
            .HasComment("Дата и время создания записи")
            .HasColumnName("created_utc")
            .IsRequired();

        builder
            .Property(x => x.ModifiedUtc)
            .HasComment("Дата и время последнего изменения записи")
            .HasColumnName("modified_utc")
            .IsRequired(false);

        builder
            .Property(x => x.IsDeleted)
            .HasComment("Признак удаления записи")
            .HasColumnName("is_deleted")
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .Property(x => x.UserId)
            .HasComment("Идентификатор пользователя")
            .HasColumnName("user_id")
            .IsRequired();

        builder
            .Property(x => x.IsRead)
            .HasComment("Прочитано уведомление или нет")
            .HasColumnName("is_read")
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .Property(x => x.Text)
            .HasComment("Текст уведомления")
            .HasColumnName("text")
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.UserId)
            .HasDatabaseName("ix_user_notifications_user_id");
    }
}
