using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedInterviewChangedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "state",
                schema: "public",
                table: "interview_versions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Состояние версии",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Состояние версии");

            migrationBuilder.AddColumn<int>(
                name: "changed_by",
                schema: "public",
                table: "interview_versions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Кто версию сделал / изменил");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "changed_by",
                schema: "public",
                table: "interview_versions");

            migrationBuilder.AlterColumn<int>(
                name: "state",
                schema: "public",
                table: "interview_versions",
                type: "integer",
                nullable: true,
                comment: "Состояние версии",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Состояние версии");
        }
    }
}
