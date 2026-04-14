using InterviewTraining.Domain;
using System;
using System.Collections.Generic;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    private static readonly SkillGroup DevelopmentGroup = new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
        Name = "Разработка",
        CreatedUtc = _utc_date,
        ModifiedUtc = _utc_date,
        IsDeleted = false
    };

    private static readonly SkillGroup AnalyticsGroup = new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
        Name = "Аналитика",
        CreatedUtc = _utc_date,
        IsDeleted = false
    };

    private static readonly SkillGroup TestingGroup = new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
        Name = "Тестирование",
        CreatedUtc = _utc_date,
        IsDeleted = false
    };

    private static readonly SkillGroup DevOpsGroup = new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
        Name = "DevOps",
        CreatedUtc = _utc_date,
        IsDeleted = false
    };

    private static readonly SkillGroup BackendGroup = new()
    {
        Id = Guid.Parse("11000000-0000-0000-0000-000000000001"),
        Name = "Backend",
        ParentGroupId = DevelopmentGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup FrontendGroup = new()
    {
        Id = Guid.Parse("11000000-0000-0000-0000-000000000002"),
        Name = "Frontend",
        ParentGroupId = DevelopmentGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup MobileGroup = new()
    {
        Id = Guid.Parse("11000000-0000-0000-0000-000000000004"),
        Name = "Mobile",
        ParentGroupId = DevelopmentGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup DotNetGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000001"),
        Name = ".NET",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup JavaGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000002"),
        Name = "Java",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup PythonGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000003"),
        Name = "Python",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup NodeJsGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000004"),
        Name = "Node.js",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup GoGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000005"),
        Name = "Go",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup PhpGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000006"),
        Name = "PHP",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup RubyGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000007"),
        Name = "Ruby",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup CppGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000008"),
        Name = "C/C++",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup RustGroup = new()
    {
        Id = Guid.Parse("11100000-0000-0000-0000-000000000009"),
        Name = "Rust",
        ParentGroupId = BackendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup ReactGroup = new()
    {
        Id = Guid.Parse("11200000-0000-0000-0000-000000000001"),
        Name = "React",
        ParentGroupId = FrontendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup AngularGroup = new()
    {
        Id = Guid.Parse("11200000-0000-0000-0000-000000000002"),
        Name = "Angular",
        ParentGroupId = FrontendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup VueGroup = new()
    {
        Id = Guid.Parse("11200000-0000-0000-0000-000000000003"),
        Name = "Vue.js",
        ParentGroupId = FrontendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup JavaScriptGroup = new()
    {
        Id = Guid.Parse("11200000-0000-0000-0000-000000000004"),
        Name = "JavaScript",
        ParentGroupId = FrontendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup TypeScriptGroup = new()
    {
        Id = Guid.Parse("11200000-0000-0000-0000-000000000005"),
        Name = "TypeScript",
        ParentGroupId = FrontendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup CssGroup = new()
    {
        Id = Guid.Parse("11200000-0000-0000-0000-000000000006"),
        Name = "CSS/Стилизация",
        ParentGroupId = FrontendGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup IosGroup = new()
    {
        Id = Guid.Parse("11400000-0000-0000-0000-000000000001"),
        Name = "iOS",
        ParentGroupId = MobileGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup AndroidGroup = new()
    {
        Id = Guid.Parse("11400000-0000-0000-0000-000000000002"),
        Name = "Android",
        ParentGroupId = MobileGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup CrossPlatformGroup = new()
    {
        Id = Guid.Parse("11400000-0000-0000-0000-000000000003"),
        Name = "Кроссплатформенная разработка",
        ParentGroupId = MobileGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup SystemAnalyticsGroup = new()
    {
        Id = Guid.Parse("12000000-0000-0000-0000-000000000001"),
        Name = "Системная аналитика",
        ParentGroupId = AnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup BusinessAnalyticsGroup = new()
    {
        Id = Guid.Parse("12000000-0000-0000-0000-000000000002"),
        Name = "Бизнес-аналитика",
        ParentGroupId = AnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup DataAnalyticsGroup = new()
    {
        Id = Guid.Parse("12000000-0000-0000-0000-000000000003"),
        Name = "Data Analytics",
        ParentGroupId = AnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup ProductManagementGroup = new()
    {
        Id = Guid.Parse("12000000-0000-0000-0000-000000000004"),
        Name = "Product Management",
        ParentGroupId = AnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup ManualTestingGroup = new()
    {
        Id = Guid.Parse("13000000-0000-0000-0000-000000000001"),
        Name = "Ручное тестирование",
        ParentGroupId = TestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup AutoTestingGroup = new()
    {
        Id = Guid.Parse("13000000-0000-0000-0000-000000000002"),
        Name = "Автотестирование",
        ParentGroupId = TestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup PerformanceTestingGroup = new()
    {
        Id = Guid.Parse("13000000-0000-0000-0000-000000000003"),
        Name = "Нагрузочное тестирование",
        ParentGroupId = TestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup SecurityTestingGroup = new()
    {
        Id = Guid.Parse("13000000-0000-0000-0000-000000000004"),
        Name = "Тестирование безопасности",
        ParentGroupId = TestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup CICDGroup = new()
    {
        Id = Guid.Parse("14000000-0000-0000-0000-000000000001"),
        Name = "CI/CD",
        ParentGroupId = DevOpsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup CloudGroup = new()
    {
        Id = Guid.Parse("14000000-0000-0000-0000-000000000002"),
        Name = "Облачные платформы",
        ParentGroupId = DevOpsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup ContainerizationGroup = new()
    {
        Id = Guid.Parse("14000000-0000-0000-0000-000000000003"),
        Name = "Контейнеризация",
        ParentGroupId = DevOpsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup MonitoringGroup = new()
    {
        Id = Guid.Parse("14000000-0000-0000-0000-000000000004"),
        Name = "Мониторинг и логирование",
        ParentGroupId = DevOpsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly SkillGroup InfrastructureGroup = new()
    {
        Id = Guid.Parse("14000000-0000-0000-0000-000000000005"),
        Name = "Инфраструктура",
        ParentGroupId = DevOpsGroup.Id,
        CreatedUtc = _utc_date
    };

    /// <summary>
    /// Языки для проведения собеседования
    /// </summary>
    private static readonly SkillGroup InterviewLanguagesGroup = new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
        Name = "Языки для проведения собеседования",
        CreatedUtc = _utc_date,
        IsDeleted = false
    };

    public static List<SkillGroup> GetAllGroups() => new()
    {
        // Root groups
        DevelopmentGroup, AnalyticsGroup, TestingGroup, DevOpsGroup,
            
        // Development subgroups
        BackendGroup, FrontendGroup, MobileGroup,
            
        // Backend subgroups
        DotNetGroup, JavaGroup, PythonGroup, NodeJsGroup, GoGroup, PhpGroup, RubyGroup, CppGroup, RustGroup,
            
        // Frontend subgroups
        ReactGroup, AngularGroup, VueGroup, JavaScriptGroup, TypeScriptGroup, CssGroup,
            
        // Mobile subgroups
        IosGroup, AndroidGroup, CrossPlatformGroup,

        // Analytics subgroups
        SystemAnalyticsGroup, BusinessAnalyticsGroup, DataAnalyticsGroup, ProductManagementGroup,
            
        // Testing subgroups
        ManualTestingGroup, AutoTestingGroup, PerformanceTestingGroup, SecurityTestingGroup,
            
        // DevOps subgroups
        CICDGroup, CloudGroup, ContainerizationGroup, MonitoringGroup, InfrastructureGroup,

        // Interview Languages
        InterviewLanguagesGroup
    };
}
