using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserChatMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_chat_messages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    receiver_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_text = table.Column<string>(type: "text", nullable: false),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_chat_messages_additional_user_infos_receiver_user_id",
                        column: x => x.receiver_user_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_chat_messages_additional_user_infos_sender_user_id",
                        column: x => x.sender_user_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_chat_messages_is_deleted",
                schema: "public",
                table: "user_chat_messages",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_user_chat_messages_receiver_user_id",
                schema: "public",
                table: "user_chat_messages",
                column: "receiver_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_chat_messages_sender_receiver",
                schema: "public",
                table: "user_chat_messages",
                columns: new[] { "sender_user_id", "receiver_user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_user_chat_messages_sender_user_id",
                schema: "public",
                table: "user_chat_messages",
                column: "sender_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_chat_messages",
                schema: "public");
        }
    }
}
