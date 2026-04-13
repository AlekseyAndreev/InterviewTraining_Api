using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public class InterviewContext : DbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options"></param>
    public InterviewContext(DbContextOptions<InterviewContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    /// <summary>
    /// Навыки
    /// </summary>
    public DbSet<Skill> Skills { get; set; }

    /// <summary>
    /// Тэги для навыков
    /// </summary>
    public DbSet<SkillTag> SkillTags { get; set; }

    /// <summary>
    /// Группы для навыков
    /// </summary>
    public DbSet<SkillGroup> SkillGroups { get; set; }

    /// <summary>
    /// Дополнительная информация пользователей
    /// </summary>
    public DbSet<AdditionalUserInfo> AdditionalUserInfos { get; set; }

    /// <summary>
    /// Рейтинги пользователей
    /// </summary>
    public DbSet<UserRating> UserRatings { get; set; }

    /// <summary>
    /// Интервью
    /// </summary>
    public DbSet<Interview> Interviews { get; set; }

    /// <summary>
    /// Версии интервью
    /// </summary>
    public DbSet<InterviewVersion> InterviewVersions { get; set; }

    /// <summary>
    /// При создании модели
    /// </summary>
    /// <param name="modelBuilder">modelBuilder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyConfigurations(modelBuilder);
    }

    /// <summary>
    /// При конфигурации
    /// </summary>
    /// <param name="optionsBuilder">optionsBuilder</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    private static void ApplyConfigurations(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");

        builder.ApplyConfiguration(new SkillConfiguration());
        builder.ApplyConfiguration(new SkillGroupConfiguration());
        builder.ApplyConfiguration(new SkillTagConfiguration());
        builder.ApplyConfiguration(new AdditionalUserInfoConfiguration());
        builder.ApplyConfiguration(new UserRatingConfiguration());
        builder.ApplyConfiguration(new InterviewConfiguration());
        builder.ApplyConfiguration(new InterviewVersionConfiguration());
    }
}
