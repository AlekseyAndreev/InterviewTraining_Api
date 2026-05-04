using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsExpertAvailableInSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_expert_available_in_search",
                schema: "public",
                table: "additional_user_infos",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак доступности эксперта в поиске");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_expert_available_in_search",
                schema: "public",
                table: "additional_user_infos");
        }
    }
}
