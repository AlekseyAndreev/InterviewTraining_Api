using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "skill_groups",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false, comment: "Наимнование группы"),
                    parent_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill_groups", x => x.id);
                    table.ForeignKey(
                        name: "FK_skill_groups_skill_groups_parent_group_id",
                        column: x => x.parent_group_id,
                        principalSchema: "public",
                        principalTable: "skill_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "skill",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false, comment: "Наимнование навыка"),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill", x => x.id);
                    table.ForeignKey(
                        name: "FK_skill_skill_groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "public",
                        principalTable: "skill_groups",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "skill_tags",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false, comment: "Наимнование группы"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_skill_tags_skill_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "public",
                        principalTable: "skill",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_skill_GroupId",
                schema: "public",
                table: "skill",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_skill_groups_parent_group_id",
                schema: "public",
                table: "skill_groups",
                column: "parent_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_skill_tags_SkillId",
                schema: "public",
                table: "skill_tags",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "skill_tags",
                schema: "public");

            migrationBuilder.DropTable(
                name: "skill",
                schema: "public");

            migrationBuilder.DropTable(
                name: "skill_groups",
                schema: "public");
        }
    }
}
