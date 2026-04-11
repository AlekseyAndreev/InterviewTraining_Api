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
                name: "additional_user_infos",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    identity_user_id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false, comment: "Идентификатор пользователя в Identity"),
                    full_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "Полное имя пользователя"),
                    photo = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "URL фото пользователя"),
                    short_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "Краткое описание"),
                    description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "Полное описание"),
                    interview_schedule_at_any_time = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Готов проводить интервью в любое время"),
                    is_candidate = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак кандидата"),
                    is_expert = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак эксперта"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additional_user_infos", x => x.id);
                });

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
                name: "user_ratings",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    user_from_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя, который поставил рейтинг"),
                    user_to_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя, которому поставили рейтинг"),
                    rating_value = table.Column<int>(type: "integer", nullable: false, comment: "Значение рейтинга от 1 до 5"),
                    comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "Комментарий к рейтингу"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_ratings", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_ratings_additional_user_infos_user_from_id",
                        column: x => x.user_from_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_ratings_additional_user_infos_user_to_id",
                        column: x => x.user_to_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "skills",
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
                    table.PrimaryKey("PK_skills", x => x.id);
                    table.ForeignKey(
                        name: "FK_skills_skill_groups_GroupId",
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
                        name: "FK_skill_tags_skills_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "public",
                        principalTable: "skills",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_additional_user_info_identity_user_id",
                schema: "public",
                table: "additional_user_infos",
                column: "identity_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_additional_user_info_is_candidate",
                schema: "public",
                table: "additional_user_infos",
                column: "is_candidate");

            migrationBuilder.CreateIndex(
                name: "ix_additional_user_info_is_expert",
                schema: "public",
                table: "additional_user_infos",
                column: "is_expert");

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

            migrationBuilder.CreateIndex(
                name: "IX_skills_GroupId",
                schema: "public",
                table: "skills",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "ix_user_rating_user_from_to",
                schema: "public",
                table: "user_ratings",
                columns: new[] { "user_from_id", "user_to_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_rating_user_to_id",
                schema: "public",
                table: "user_ratings",
                column: "user_to_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "skill_tags",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_ratings",
                schema: "public");

            migrationBuilder.DropTable(
                name: "skills",
                schema: "public");

            migrationBuilder.DropTable(
                name: "additional_user_infos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "skill_groups",
                schema: "public");
        }
    }
}
