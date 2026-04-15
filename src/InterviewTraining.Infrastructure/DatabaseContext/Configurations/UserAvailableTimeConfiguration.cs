using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности UserAvailableTime
/// </summary>
public class UserAvailableTimeConfiguration : IEntityTypeConfiguration<Domain.UserAvailableTime>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.UserAvailableTime> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("user_available_times");
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
            .Property(x => x.UserId)
            .HasComment("Идентификатор пользователя (эксперта)")
            .HasColumnName("user_id")
            .IsRequired();

        builder
            .Property(x => x.AvailabilityType)
            .HasComment("Тип доступности: 0=AlwaysAvailable, 1=WeeklyFullDay, 2=WeeklyWithTime, 3=SpecificDateTime")
            .HasColumnName("availability_type")
            .IsRequired();

        builder
            .Property(x => x.DayOfWeek)
            .HasComment("День недели (0-6), если применимо")
            .HasColumnName("day_of_week")
            .IsRequired(false);

        builder
            .Property(x => x.SpecificDate)
            .HasComment("Конкретная дата, если применимо")
            .HasColumnName("specific_date")
            .IsRequired(false);

        builder
            .Property(x => x.StartTime)
            .HasComment("Время начала в UTC")
            .HasColumnName("start_time")
            .IsRequired(false);

        builder
            .Property(x => x.EndTime)
            .HasComment("Время окончания в UTC")
            .HasColumnName("end_time")
            .IsRequired(false);

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.UserId)
            .HasDatabaseName("ix_user_available_times_user_id");

        builder
            .HasIndex(x => x.AvailabilityType)
            .HasDatabaseName("ix_user_available_times_availability_type");
    }
}
