using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameInterviewChatMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_messages",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "interview_chat_messages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор сообщения"),
                    interview_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор интервью"),
                    sender_type = table.Column<int>(type: "integer", nullable: false, comment: "Тип отправителя сообщения"),
                    sender_user_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор пользователя-отправителя (null для системных сообщений)"),
                    message_text = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false, comment: "Текст сообщения"),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Признак того, что сообщение было отредактировано"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_interview_chat_messages_additional_user_infos_sender_user_id",
                        column: x => x.sender_user_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_interview_chat_messages_interviews_interview_id",
                        column: x => x.interview_id,
                        principalSchema: "public",
                        principalTable: "interviews",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_chat_messages_interview_id",
                schema: "public",
                table: "interview_chat_messages",
                column: "interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_chat_messages_sender_user_id",
                schema: "public",
                table: "interview_chat_messages",
                column: "sender_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interview_chat_messages",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "chat_messages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор сообщения"),
                    interview_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор интервью"),
                    sender_user_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор пользователя-отправителя (null для системных сообщений)"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Признак того, что сообщение было отредактировано"),
                    message_text = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false, comment: "Текст сообщения"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    sender_type = table.Column<int>(type: "integer", nullable: false, comment: "Тип отправителя сообщения")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_chat_messages_additional_user_infos_sender_user_id",
                        column: x => x.sender_user_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_chat_messages_interviews_interview_id",
                        column: x => x.interview_id,
                        principalSchema: "public",
                        principalTable: "interviews",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_chat_messages_interview_id",
                schema: "public",
                table: "chat_messages",
                column: "interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_messages_sender_user_id",
                schema: "public",
                table: "chat_messages",
                column: "sender_user_id");
        }
    }
}
