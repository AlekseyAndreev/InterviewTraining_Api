using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности Interview
/// </summary>
public class InterviewConfiguration : IEntityTypeConfiguration<Domain.Interview>
{
    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.Interview> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("interviews");
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
            .Property(x => x.CandidateId)
            .HasComment("Идентификатор кандидата")
            .HasColumnName("candidate_id")
            .IsRequired();

        builder
            .Property(x => x.ExpertId)
            .HasComment("Идентификатор эксперта")
            .HasColumnName("expert_id")
            .IsRequired();

        builder
            .Property(x => x.ActiveInterviewVersionId)
            .HasComment("Идентификатор активной версии интервью")
            .HasColumnName("active_interview_version_id")
            .IsRequired(false);

        builder
            .HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey(x => x.CandidateId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(x => x.Expert)
            .WithMany()
            .HasForeignKey(x => x.ExpertId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(x => x.ActiveInterviewVersion)
            .WithMany()
            .HasForeignKey(x => x.ActiveInterviewVersionId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasMany(x => x.Versions)
            .WithOne(x => x.Interview)
            .HasForeignKey(x => x.InterviewId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasMany(x => x.ChatMessages)
            .WithOne(x => x.Interview)
            .HasForeignKey(x => x.InterviewId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasIndex(x => x.CandidateId)
            .HasDatabaseName("ix_interviews_candidate_id");

        builder
            .HasIndex(x => x.ExpertId)
            .HasDatabaseName("ix_interviews_expert_id");

        builder
            .HasIndex(x => x.ActiveInterviewVersionId)
            .HasDatabaseName("ix_interviews_active_interview_version_id");
    }
}
