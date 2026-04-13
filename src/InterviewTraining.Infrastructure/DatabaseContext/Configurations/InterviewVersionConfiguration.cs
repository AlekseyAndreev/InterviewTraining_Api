using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности InterviewVersion
/// </summary>
public class InterviewVersionConfiguration : IEntityTypeConfiguration<Domain.InterviewVersion>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.InterviewVersion> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("interview_versions");
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
            .Property(x => x.InterviewId)
            .HasComment("Идентификатор интервью")
            .HasColumnName("interview_id")
            .IsRequired();

        builder
            .Property(x => x.LinkToVideoCall)
            .HasComment("Ссылка на видеозвонок")
            .HasColumnName("link_to_video_call")
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.OwnsOne(x => x.Candidate, navigationBuilder =>
        {
            navigationBuilder
                .Property(x => x.IsApproved)
                .HasComment("Признак подтверждения кандидатом")
                .HasColumnName("candidate_is_approved")
                .HasDefaultValue(false)
                .IsRequired();

            navigationBuilder
                .Property(x => x.IsPaid)
                .HasComment("Признак оплаты кандидатом")
                .HasColumnName("candidate_is_paid")
                .HasDefaultValue(false)
                .IsRequired();

            navigationBuilder
                .Property(x => x.IsCancelled)
                .HasComment("Признак отмены кандидатом")
                .HasColumnName("candidate_is_cancelled")
                .HasDefaultValue(false)
                .IsRequired();

            navigationBuilder
                .Property(x => x.CancellReason)
                .HasComment("Причина отмены кандидатом")
                .HasColumnName("candidate_cancell_reason")
                .IsRequired(false)
                .HasMaxLength(2000);
        });

        builder.OwnsOne(x => x.Expert, navigationBuilder =>
        {
            navigationBuilder
                .Property(x => x.IsApproved)
                .HasComment("Признак подтверждения экспертом")
                .HasColumnName("expert_is_approved")
                .HasDefaultValue(false)
                .IsRequired();

            navigationBuilder
                .Property(x => x.IsPaid)
                .HasComment("Признак оплаты экспертом")
                .HasColumnName("expert_is_paid")
                .HasDefaultValue(false)
                .IsRequired();

            navigationBuilder
                .Property(x => x.IsCancelled)
                .HasComment("Признак отмены экспертом")
                .HasColumnName("expert_is_cancelled")
                .HasDefaultValue(false)
                .IsRequired();

            navigationBuilder
                .Property(x => x.CancellReason)
                .HasComment("Причина отмены экспертом")
                .HasColumnName("expert_cancell_reason")
                .IsRequired(false)
                .HasMaxLength(2000);
        });

        builder
            .HasOne(x => x.Interview)
            .WithMany(x => x.Versions)
            .HasForeignKey(x => x.InterviewId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasIndex(x => x.InterviewId)
            .HasDatabaseName("ix_interview_versions_interview_id");
    }
}
