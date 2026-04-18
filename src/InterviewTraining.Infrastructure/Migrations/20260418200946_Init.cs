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
                name: "currencies",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false, comment: "Код валюты (ISO 4217)"),
                    name_ru = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Название валюты на русском языке"),
                    name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Название валюты на английском языке"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "interview_languages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, comment: "Код"),
                    name_ru = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Наименование языка по русски"),
                    name_en = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Наименование языка по английски"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_languages", x => x.id);
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
                name: "time_zones",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Код часового пояса"),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: "Наименование часового пояса"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_zones", x => x.id);
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
                name: "additional_user_infos",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    identity_user_id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false, comment: "Идентификатор пользователя в Identity"),
                    full_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "Полное имя пользователя"),
                    photo_local = table.Column<byte[]>(type: "bytea", nullable: true, comment: "Фото пользователя"),
                    short_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "Краткое описание"),
                    description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "Полное описание"),
                    is_candidate = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак кандидата"),
                    is_expert = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак эксперта"),
                    time_zone_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор часового пояса пользователя"),
                    interview_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true, comment: "Сумма оплаты за собеседование"),
                    currency_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор валюты оплаты"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additional_user_infos", x => x.id);
                    table.ForeignKey(
                        name: "FK_additional_user_infos_time_zones_time_zone_id",
                        column: x => x.time_zone_id,
                        principalSchema: "public",
                        principalTable: "time_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_additional_user_info_currency",
                        column: x => x.currency_id,
                        principalSchema: "public",
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.CreateTable(
                name: "user_available_times",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя (эксперта)"),
                    availability_type = table.Column<int>(type: "integer", nullable: false, comment: "Тип доступности: 0=AlwaysAvailable, 1=WeeklyFullDay, 2=WeeklyWithTime, 3=SpecificDateTime"),
                    day_of_week = table.Column<int>(type: "integer", nullable: true, comment: "День недели (0-6), если применимо"),
                    specific_date = table.Column<DateOnly>(type: "date", nullable: true, comment: "Конкретная дата, если применимо"),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true, comment: "Время начала в UTC"),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true, comment: "Время окончания в UTC"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалена запись или нет")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_available_times", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_available_times_additional_user_infos_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "user_skills",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя"),
                    skill_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор навыка"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_skills", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_skills_additional_user_infos_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_skills_skills_skill_id",
                        column: x => x.skill_id,
                        principalSchema: "public",
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interview_versions",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    interview_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор интервью"),
                    candidate_is_paid_by = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак оплаты кандидатом"),
                    notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "Примечания от кандидата при бронировании"),
                    candidate_is_rescheduled = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак переноса времени кандидатом"),
                    candidate_is_approved = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак подтверждения кандидатом"),
                    candidate_is_cancelled = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак отмены кандидатом"),
                    candidate_cancel_reason = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "Причина отмены кандидатом"),
                    candidate_is_deleted = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак удаления кандидатом"),
                    expert_is_paid_to = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак оплаты эксперту"),
                    expert_is_rescheduled = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак переноса времени экспертом"),
                    expert_is_approved = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак подтверждения экспертом"),
                    expert_is_cancelled = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак отмены экспертом"),
                    expert_cancel_reason = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "Причина отмены экспертом"),
                    expert_is_deleted = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Признак удаления экспертом"),
                    IsAdminApproved = table.Column<bool>(type: "boolean", nullable: false),
                    link_to_video_call = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "Ссылка на видеозвонок"),
                    start_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Начало собеседования в UTC"),
                    end_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Конец собеседования в UTC"),
                    language_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор языка"),
                    interview_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true, comment: "Сумма оплаты за собеседование"),
                    currency_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор валюты"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_versions", x => x.id);
                    table.ForeignKey(
                        name: "FK_interview_versions_currencies_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "public",
                        principalTable: "currencies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_interview_versions_interview_languages_language_id",
                        column: x => x.language_id,
                        principalSchema: "public",
                        principalTable: "interview_languages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "interviews",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор"),
                    candidate_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор кандидата"),
                    expert_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор эксперта"),
                    active_interview_version_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор активной версии интервью"),
                    created_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время создания записи в таблице"),
                    modified_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Дата и время последнего изменения записи в таблице")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interviews", x => x.id);
                    table.ForeignKey(
                        name: "FK_interviews_additional_user_infos_candidate_id",
                        column: x => x.candidate_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_interviews_additional_user_infos_expert_id",
                        column: x => x.expert_id,
                        principalSchema: "public",
                        principalTable: "additional_user_infos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_interviews_interview_versions_active_interview_version_id",
                        column: x => x.active_interview_version_id,
                        principalSchema: "public",
                        principalTable: "interview_versions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_additional_user_info_identity_user_id",
                schema: "public",
                table: "additional_user_infos",
                column: "identity_user_id",
                unique: true);

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
                name: "IX_additional_user_infos_currency_id",
                schema: "public",
                table: "additional_user_infos",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_additional_user_infos_time_zone_id",
                schema: "public",
                table: "additional_user_infos",
                column: "time_zone_id");

            migrationBuilder.CreateIndex(
                name: "ix_currencies_code",
                schema: "public",
                table: "currencies",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_interview_versions_currency_id",
                schema: "public",
                table: "interview_versions",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_interview_versions_interview_id",
                schema: "public",
                table: "interview_versions",
                column: "interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_versions_language_id",
                schema: "public",
                table: "interview_versions",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_interviews_active_interview_version_id",
                schema: "public",
                table: "interviews",
                column: "active_interview_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_interviews_candidate_id",
                schema: "public",
                table: "interviews",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "ix_interviews_expert_id",
                schema: "public",
                table: "interviews",
                column: "expert_id");

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
                name: "ix_time_zones_code",
                schema: "public",
                table: "time_zones",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_available_times_availability_type",
                schema: "public",
                table: "user_available_times",
                column: "availability_type");

            migrationBuilder.CreateIndex(
                name: "ix_user_available_times_user_id",
                schema: "public",
                table: "user_available_times",
                column: "user_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_user_skills_skill_id",
                schema: "public",
                table: "user_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_skills_user_id",
                schema: "public",
                table: "user_skills",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_skills_user_id_skill_id",
                schema: "public",
                table: "user_skills",
                columns: new[] { "user_id", "skill_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_interview_versions_interviews_interview_id",
                schema: "public",
                table: "interview_versions",
                column: "interview_id",
                principalSchema: "public",
                principalTable: "interviews",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_additional_user_infos_time_zones_time_zone_id",
                schema: "public",
                table: "additional_user_infos");

            migrationBuilder.DropForeignKey(
                name: "fk_additional_user_info_currency",
                schema: "public",
                table: "additional_user_infos");

            migrationBuilder.DropForeignKey(
                name: "FK_interview_versions_currencies_currency_id",
                schema: "public",
                table: "interview_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_interview_versions_interview_languages_language_id",
                schema: "public",
                table: "interview_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_interview_versions_interviews_interview_id",
                schema: "public",
                table: "interview_versions");

            migrationBuilder.DropTable(
                name: "skill_tags",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_available_times",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_ratings",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_skills",
                schema: "public");

            migrationBuilder.DropTable(
                name: "skills",
                schema: "public");

            migrationBuilder.DropTable(
                name: "skill_groups",
                schema: "public");

            migrationBuilder.DropTable(
                name: "time_zones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "interview_languages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "interviews",
                schema: "public");

            migrationBuilder.DropTable(
                name: "additional_user_infos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "interview_versions",
                schema: "public");
        }
    }
}
