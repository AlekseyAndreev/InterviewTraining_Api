using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

/// <summary>
/// Конфигурация сущности InterviewChatMessage
/// </summary>
public class InterviewChatMessageConfiguration : IEntityTypeConfiguration<Domain.InterviewChatMessage>
{
    /// <summary>
    /// Configure
    /// </summary>
    ///<param name="builder">builder</param>
    public void Configure(EntityTypeBuilder<Domain.InterviewChatMessage> builder)
    {
        builder
            .ToTable(t =>
            {
                t.Metadata.SetTableName("interview_chat_messages");
                t.Metadata.SetSchema(null);
            });

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasComment("Уникальный идентификатор сообщения")
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
            .Property(x => x.SenderType)
            .HasComment("Тип отправителя сообщения")
            .HasColumnName("sender_type")
            .IsRequired();

        builder
            .Property(x => x.SenderUserId)
            .HasComment("Идентификатор пользователя-отправителя (null для системных сообщений)")
            .HasColumnName("sender_user_id")
            .IsRequired(false);

        builder
            .Property(x => x.MessageText)
            .HasComment("Текст сообщения")
            .HasColumnName("message_text")
            .IsRequired()
            .HasMaxLength(4000);

        builder
            .Property(x => x.IsEdited)
            .HasComment("Признак того, что сообщение было отредактировано")
            .HasColumnName("is_edited")
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .HasOne(x => x.Interview)
            .WithMany(x => x.InterviewChatMessages)
            .HasForeignKey(x => x.InterviewId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasIndex(x => x.InterviewId)
            .HasDatabaseName("ix_chat_messages_interview_id");
    }
}
