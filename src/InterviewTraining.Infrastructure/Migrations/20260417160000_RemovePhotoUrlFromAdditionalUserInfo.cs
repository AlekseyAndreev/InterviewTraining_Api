using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhotoUrlFromAdditionalUserInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_url",
                schema: "public",
                table: "additional_user_infos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                schema: "public",
                table: "additional_user_infos",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "URL фото пользователя");
        }
    }
}
