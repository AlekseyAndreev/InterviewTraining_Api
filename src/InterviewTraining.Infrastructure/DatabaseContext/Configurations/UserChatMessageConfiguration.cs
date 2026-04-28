using InterviewTraining.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewTraining.Infrastructure.DatabaseContext.Configurations;

///<summary>
/// EF configuration for UserChatMessage
///</summary>
public class UserChatMessageConfiguration : IEntityTypeConfiguration<UserChatMessage>
{
    ///<summary>
    /// Configure
    ///</summary>
    public void Configure(EntityTypeBuilder<UserChatMessage> builder)
    {
        builder.ToTable("user_chat_messages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.CreatedUtc)
            .HasColumnName("created_utc")
            .IsRequired();

        builder.Property(x => x.ModifiedUtc)
            .HasColumnName("updated_utc");

        builder.Property(x => x.SenderUserId)
            .HasColumnName("sender_user_id")
            .IsRequired();

        builder.Property(x => x.ReceiverUserId)
            .HasColumnName("receiver_user_id")
            .IsRequired();

        builder.Property(x => x.MessageText)
            .HasColumnName("message_text")
            .IsRequired();

        builder.Property(x => x.IsEdited)
            .HasColumnName("is_edited")
            .HasDefaultValue(false);

        builder.Property(x => x.IsRead)
            .HasColumnName("is_read")
            .HasDefaultValue(false);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.SenderUser)
            .WithMany()
            .HasForeignKey(x => x.SenderUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ReceiverUser)
            .WithMany()
            .HasForeignKey(x => x.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.SenderUserId)
            .HasDatabaseName("ix_user_chat_messages_sender_user_id");

        builder.HasIndex(x => x.ReceiverUserId)
            .HasDatabaseName("ix_user_chat_messages_receiver_user_id");

        builder.HasIndex(x => new { x.SenderUserId, x.ReceiverUserId })
            .HasDatabaseName("ix_user_chat_messages_sender_receiver");

        builder.HasIndex(x => x.IsDeleted)
            .HasDatabaseName("ix_user_chat_messages_is_deleted");
    }
}
