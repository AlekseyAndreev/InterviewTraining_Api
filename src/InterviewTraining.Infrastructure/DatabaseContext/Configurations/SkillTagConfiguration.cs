using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

public class SkillTagConfiguration : IEntityTypeConfiguration<Domain.SkillTag>
{
    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.SkillTag> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("skill_tags");
                t.Metadata.SetSchema(null);
            });

        builder.HasKey(x => x.Id);

        builder
            .Property(action => action.Id)
            .HasComment("Уникальный идентификатор")
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(action => action.CreatedUtc)
            .HasComment("Дата и время создания записи в таблице")
            .HasColumnName("created_utc")
            .IsRequired();

        builder
            .Property(action => action.ModifiedUtc)
            .HasComment("Дата и время последнего изменения записи в таблице")
            .HasColumnName("modified_utc")
            .IsRequired(false);

        builder
            .Property(action => action.IsDeleted)
            .HasComment("Признак удалена запись или нет")
            .HasColumnName("is_deleted")
            .IsRequired();

        builder
            .Property(action => action.Name)
            .HasComment("Наимнование группы")
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(2000);

        builder
            .HasOne(action => action.Skill)
            .WithMany(process => process.Tags)
            .HasForeignKey(action => action.SkillId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}