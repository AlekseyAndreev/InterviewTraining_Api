using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLinkToUserNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link_id",
                schema: "public",
                table: "user_notifications",
                type: "text",
                nullable: true,
                comment: "Идентификатор для ссылки");

            migrationBuilder.AddColumn<string>(
                name: "link_type",
                schema: "public",
                table: "user_notifications",
                type: "text",
                nullable: true,
                comment: "Вид ссылки");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link_id",
                schema: "public",
                table: "user_notifications");

            migrationBuilder.DropColumn(
                name: "link_type",
                schema: "public",
                table: "user_notifications");
        }
    }
}
