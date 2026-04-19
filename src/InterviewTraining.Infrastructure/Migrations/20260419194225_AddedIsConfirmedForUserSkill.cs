using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsConfirmedForUserSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_confirmed",
                schema: "public",
                table: "skills");

            migrationBuilder.AddColumn<bool>(
                name: "is_confirmed",
                schema: "public",
                table: "user_skills",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак подтверждён навык у пользователя или нет");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_confirmed",
                schema: "public",
                table: "user_skills");

            migrationBuilder.AddColumn<bool>(
                name: "is_confirmed",
                schema: "public",
                table: "skills",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак подтверждён навык или нет");
        }
    }
}
