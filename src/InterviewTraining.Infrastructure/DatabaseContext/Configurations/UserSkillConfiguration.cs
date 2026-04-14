using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности UserSkill
/// </summary>
public class UserSkillConfiguration : IEntityTypeConfiguration<Domain.UserSkill>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.UserSkill> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("user_skills");
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
            .HasComment("Идентификатор пользователя")
            .HasColumnName("user_id")
            .IsRequired();

        builder
            .Property(x => x.SkillId)
            .HasComment("Идентификатор навыка")
            .HasColumnName("skill_id")
            .IsRequired();

        // Связь с пользователем
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь с навыком
        builder
            .HasOne(x => x.Skill)
            .WithMany()
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.Cascade);

        // Составной уникальный индекс для предотвращения дублирования
        builder
            .HasIndex(x => new { x.UserId, x.SkillId })
            .IsUnique()
            .HasDatabaseName("ix_user_skills_user_id_skill_id");

        // Индекс для быстрого поиска по UserId
        builder
            .HasIndex(x => x.UserId)
            .HasDatabaseName("ix_user_skills_user_id");

        // Индекс для быстрого поиска по SkillId
        builder
            .HasIndex(x => x.SkillId)
            .HasDatabaseName("ix_user_skills_skill_id");
    }
}
