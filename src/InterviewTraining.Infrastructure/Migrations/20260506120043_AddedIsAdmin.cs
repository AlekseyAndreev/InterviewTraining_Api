using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_admin",
                schema: "public",
                table: "additional_user_infos",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак админа");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_admin",
                schema: "public",
                table: "additional_user_infos");
        }
    }
}
