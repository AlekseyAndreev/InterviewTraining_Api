using InterviewTraining.Domain;
using System;
using System.Collections.Generic;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    private static readonly Skill CSharpSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
        Name = "C#",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AspNetCoreSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
        Name = "ASP.NET Core",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill EntityFrameworkSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
        Name = "Entity Framework",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill BlazorSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
        Name = "Blazor",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill WpfSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
        Name = "WPF",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill XamarinSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000006"),
        Name = "Xamarin",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill MauiSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000007"),
        Name = ".NET MAUI",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill LinqSkill = new()
    {
        Id = Guid.Parse("20000000-0000-0000-0000-000000000008"),
        Name = "LINQ",
        GroupId = DotNetGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JavaCoreSkill = new()
    {
        Id = Guid.Parse("20100000-0000-0000-0000-000000000001"),
        Name = "Java Core",
        GroupId = JavaGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SpringFrameworkSkill = new()
    {
        Id = Guid.Parse("20100000-0000-0000-0000-000000000002"),
        Name = "Spring Framework",
        GroupId = JavaGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SpringBootSkill = new()
    {
        Id = Guid.Parse("20100000-0000-0000-0000-000000000003"),
        Name = "Spring Boot",
        GroupId = JavaGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill HibernateSkill = new()
    {
        Id = Guid.Parse("20100000-0000-0000-0000-000000000004"),
        Name = "Hibernate",
        GroupId = JavaGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill KotlinSkill = new()
    {
        Id = Guid.Parse("20100000-0000-0000-0000-000000000005"),
        Name = "Kotlin",
        GroupId = JavaGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill MavenSkill = new()
    {
        Id = Guid.Parse("20100000-0000-0000-0000-000000000006"),
        Name = "Maven",
        GroupId = JavaGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GradleSkill = new()
    {
        Id = Guid.Parse("20100000-0000-0000-0000-000000000007"),
        Name = "Gradle",
        GroupId = JavaGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PythonCoreSkill = new()
    {
        Id = Guid.Parse("20200000-0000-0000-0000-000000000001"),
        Name = "Python",
        GroupId = PythonGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill DjangoSkill = new()
    {
        Id = Guid.Parse("20200000-0000-0000-0000-000000000002"),
        Name = "Django",
        GroupId = PythonGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill FastApiSkill = new()
    {
        Id = Guid.Parse("20200000-0000-0000-0000-000000000003"),
        Name = "FastAPI",
        GroupId = PythonGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill FlaskSkill = new()
    {
        Id = Guid.Parse("20200000-0000-0000-0000-000000000004"),
        Name = "Flask",
        GroupId = PythonGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PandasSkill = new()
    {
        Id = Guid.Parse("20200000-0000-0000-0000-000000000005"),
        Name = "Pandas",
        GroupId = PythonGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NumPySkill = new()
    {
        Id = Guid.Parse("20200000-0000-0000-0000-000000000006"),
        Name = "NumPy",
        GroupId = PythonGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NodeJsCoreSkill = new()
    {
        Id = Guid.Parse("20300000-0000-0000-0000-000000000001"),
        Name = "Node.js",
        GroupId = NodeJsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ExpressSkill = new()
    {
        Id = Guid.Parse("20300000-0000-0000-0000-000000000002"),
        Name = "Express.js",
        GroupId = NodeJsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NestJsSkill = new()
    {
        Id = Guid.Parse("20300000-0000-0000-0000-000000000003"),
        Name = "NestJS",
        GroupId = NodeJsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NextJsSkill = new()
    {
        Id = Guid.Parse("20300000-0000-0000-0000-000000000004"),
        Name = "Next.js",
        GroupId = NodeJsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GoCoreSkill = new()
    {
        Id = Guid.Parse("20400000-0000-0000-0000-000000000001"),
        Name = "Go",
        GroupId = GoGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GinSkill = new()
    {
        Id = Guid.Parse("20400000-0000-0000-0000-000000000002"),
        Name = "Gin",
        GroupId = GoGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PhpCoreSkill = new()
    {
        Id = Guid.Parse("20500000-0000-0000-0000-000000000001"),
        Name = "PHP",
        GroupId = PhpGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill LaravelSkill = new()
    {
        Id = Guid.Parse("20500000-0000-0000-0000-000000000002"),
        Name = "Laravel",
        GroupId = PhpGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SymfonySkill = new()
    {
        Id = Guid.Parse("20500000-0000-0000-0000-000000000003"),
        Name = "Symfony",
        GroupId = PhpGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill YiiSkill = new()
    {
        Id = Guid.Parse("20500000-0000-0000-0000-000000000004"),
        Name = "Yii",
        GroupId = PhpGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill RubyCoreSkill = new()
    {
        Id = Guid.Parse("20600000-0000-0000-0000-000000000001"),
        Name = "Ruby",
        GroupId = RubyGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill RailsSkill = new()
    {
        Id = Guid.Parse("20600000-0000-0000-0000-000000000002"),
        Name = "Ruby on Rails",
        GroupId = RubyGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill CppSkill = new()
    {
        Id = Guid.Parse("20700000-0000-0000-0000-000000000001"),
        Name = "C++",
        GroupId = CppGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill CSkill = new()
    {
        Id = Guid.Parse("20700000-0000-0000-0000-000000000002"),
        Name = "C",
        GroupId = CppGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill QtSkill = new()
    {
        Id = Guid.Parse("20700000-0000-0000-0000-000000000003"),
        Name = "Qt",
        GroupId = CppGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill RustCoreSkill = new()
    {
        Id = Guid.Parse("20800000-0000-0000-0000-000000000001"),
        Name = "Rust",
        GroupId = RustGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill TokioSkill = new()
    {
        Id = Guid.Parse("20800000-0000-0000-0000-000000000002"),
        Name = "Tokio",
        GroupId = RustGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ReactCoreSkill = new()
    {
        Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
        Name = "React",
        GroupId = ReactGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ReduxSkill = new()
    {
        Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
        Name = "Redux",
        GroupId = ReactGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ReactRouterSkill = new()
    {
        Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
        Name = "React Router",
        GroupId = ReactGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ReactQuerySkill = new()
    {
        Id = Guid.Parse("30000000-0000-0000-0000-000000000004"),
        Name = "React Query",
        GroupId = ReactGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NextJsFrontSkill = new()
    {
        Id = Guid.Parse("30000000-0000-0000-0000-000000000005"),
        Name = "Next.js",
        GroupId = ReactGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AngularCoreSkill = new()
    {
        Id = Guid.Parse("30100000-0000-0000-0000-000000000001"),
        Name = "Angular",
        GroupId = AngularGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill RxJsSkill = new()
    {
        Id = Guid.Parse("30100000-0000-0000-0000-000000000002"),
        Name = "RxJS",
        GroupId = AngularGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NgRxSkill = new()
    {
        Id = Guid.Parse("30100000-0000-0000-0000-000000000003"),
        Name = "NgRx",
        GroupId = AngularGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill VueCoreSkill = new()
    {
        Id = Guid.Parse("30200000-0000-0000-0000-000000000001"),
        Name = "Vue.js",
        GroupId = VueGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill VuexSkill = new()
    {
        Id = Guid.Parse("30200000-0000-0000-0000-000000000002"),
        Name = "Vuex",
        GroupId = VueGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PiniaSkill = new()
    {
        Id = Guid.Parse("30200000-0000-0000-0000-000000000003"),
        Name = "Pinia",
        GroupId = VueGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NuxtSkill = new()
    {
        Id = Guid.Parse("30200000-0000-0000-0000-000000000004"),
        Name = "Nuxt.js",
        GroupId = VueGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JsCoreSkill = new()
    {
        Id = Guid.Parse("30300000-0000-0000-0000-000000000001"),
        Name = "JavaScript",
        GroupId = JavaScriptGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill Es6Skill = new()
    {
        Id = Guid.Parse("30300000-0000-0000-0000-000000000002"),
        Name = "ES6+",
        GroupId = JavaScriptGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill WebpackSkill = new()
    {
        Id = Guid.Parse("30300000-0000-0000-0000-000000000003"),
        Name = "Webpack",
        GroupId = JavaScriptGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ViteSkill = new()
    {
        Id = Guid.Parse("30300000-0000-0000-0000-000000000004"),
        Name = "Vite",
        GroupId = JavaScriptGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill TsCoreSkill = new()
    {
        Id = Guid.Parse("30400000-0000-0000-0000-000000000001"),
        Name = "TypeScript",
        GroupId = TypeScriptGroup.Id,
        CreatedUtc = _utc_date
    };
    
    private static readonly Skill CssCoreSkill = new()
    {
        Id = Guid.Parse("30500000-0000-0000-0000-000000000001"),
        Name = "CSS3",
        GroupId = CssGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SassSkill = new()
    {
        Id = Guid.Parse("30500000-0000-0000-0000-000000000002"),
        Name = "Sass/SCSS",
        GroupId = CssGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill TailwindSkill = new()
    {
        Id = Guid.Parse("30500000-0000-0000-0000-000000000003"),
        Name = "Tailwind CSS",
        GroupId = CssGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill BootstrapSkill = new()
    {
        Id = Guid.Parse("30500000-0000-0000-0000-000000000004"),
        Name = "Bootstrap",
        GroupId = CssGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill StyledComponentsSkill = new()
    {
        Id = Guid.Parse("30500000-0000-0000-0000-000000000005"),
        Name = "Styled Components",
        GroupId = CssGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SwiftSkill = new()
    {
        Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
        Name = "Swift",
        GroupId = IosGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SwiftUI = new()
    {
        Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
        Name = "SwiftUI",
        GroupId = IosGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill UIKitSkill = new()
    {
        Id = Guid.Parse("40000000-0000-0000-0000-000000000003"),
        Name = "UIKit",
        GroupId = IosGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ObjectiveCSkill = new()
    {
        Id = Guid.Parse("40000000-0000-0000-0000-000000000004"),
        Name = "Objective-C",
        GroupId = IosGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AndroidKotlinSkill = new()
    {
        Id = Guid.Parse("40100000-0000-0000-0000-000000000001"),
        Name = "Android (Kotlin)",
        GroupId = AndroidGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JetpackComposeSkill = new()
    {
        Id = Guid.Parse("40100000-0000-0000-0000-000000000002"),
        Name = "Jetpack Compose",
        GroupId = AndroidGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AndroidXmlSkill = new()
    {
        Id = Guid.Parse("40100000-0000-0000-0000-000000000003"),
        Name = "Android XML Layouts",
        GroupId = AndroidGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill FlutterSkill = new()
    {
        Id = Guid.Parse("40200000-0000-0000-0000-000000000001"),
        Name = "Flutter",
        GroupId = CrossPlatformGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill DartSkill = new()
    {
        Id = Guid.Parse("40200000-0000-0000-0000-000000000002"),
        Name = "Dart",
        GroupId = CrossPlatformGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ReactNativeSkill = new()
    {
        Id = Guid.Parse("40200000-0000-0000-0000-000000000003"),
        Name = "React Native",
        GroupId = CrossPlatformGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill UmlSkill = new()
    {
        Id = Guid.Parse("50000000-0000-0000-0000-000000000001"),
        Name = "UML",
        GroupId = SystemAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill BpmnSkill = new()
    {
        Id = Guid.Parse("50000000-0000-0000-0000-000000000002"),
        Name = "BPMN",
        GroupId = SystemAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SqlAnalyticsSkill = new()
    {
        Id = Guid.Parse("50000000-0000-0000-0000-000000000003"),
        Name = "SQL для аналитики",
        GroupId = SystemAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ApiDesignSkill = new()
    {
        Id = Guid.Parse("50000000-0000-0000-0000-000000000004"),
        Name = "API Design",
        GroupId = SystemAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill SwaggerSkill = new()
    {
        Id = Guid.Parse("50000000-0000-0000-0000-000000000005"),
        Name = "Swagger/OpenAPI",
        GroupId = SystemAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ArchimateSkill = new()
    {
        Id = Guid.Parse("50000000-0000-0000-0000-000000000006"),
        Name = "ArchiMate",
        GroupId = SystemAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill RequirementsGatheringSkill = new()
    {
        Id = Guid.Parse("50100000-0000-0000-0000-000000000001"),
        Name = "Сбор требований",
        GroupId = BusinessAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill UserStoriesSkill = new()
    {
        Id = Guid.Parse("50100000-0000-0000-0000-000000000002"),
        Name = "User Stories",
        GroupId = BusinessAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ProcessModelingSkill = new()
    {
        Id = Guid.Parse("50100000-0000-0000-0000-000000000003"),
        Name = "Моделирование процессов",
        GroupId = BusinessAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill BusinessProcessSkill = new()
    {
        Id = Guid.Parse("50100000-0000-0000-0000-000000000004"),
        Name = "Business Process Analysis",
        GroupId = BusinessAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill CompetitorsAnalysisSkill = new()
    {
        Id = Guid.Parse("50100000-0000-0000-0000-000000000005"),
        Name = "Конкурентный анализ",
        GroupId = BusinessAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PowerBISkill = new()
    {
        Id = Guid.Parse("50200000-0000-0000-0000-000000000001"),
        Name = "Power BI",
        GroupId = DataAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill TableauSkill = new()
    {
        Id = Guid.Parse("50200000-0000-0000-0000-000000000002"),
        Name = "Tableau",
        GroupId = DataAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ExcelAnalyticsSkill = new()
    {
        Id = Guid.Parse("50200000-0000-0000-0000-000000000003"),
        Name = "Excel Analytics",
        GroupId = DataAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PythonDataSkill = new()
    {
        Id = Guid.Parse("50200000-0000-0000-0000-000000000004"),
        Name = "Python для анализа данных",
        GroupId = DataAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill MachineLearningSkill = new()
    {
        Id = Guid.Parse("50200000-0000-0000-0000-000000000005"),
        Name = "Machine Learning",
        GroupId = DataAnalyticsGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ProductStrategySkill = new()
    {
        Id = Guid.Parse("50300000-0000-0000-0000-000000000001"),
        Name = "Product Strategy",
        GroupId = ProductManagementGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill RoadmapSkill = new()
    {
        Id = Guid.Parse("50300000-0000-0000-0000-000000000002"),
        Name = "Roadmapping",
        GroupId = ProductManagementGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ScrumSkill = new()
    {
        Id = Guid.Parse("50300000-0000-0000-0000-000000000003"),
        Name = "Scrum",
        GroupId = ProductManagementGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill KanbanSkill = new()
    {
        Id = Guid.Parse("50300000-0000-0000-0000-000000000004"),
        Name = "Kanban",
        GroupId = ProductManagementGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill FunctionalTestingSkill = new()
    {
        Id = Guid.Parse("60000000-0000-0000-0000-000000000001"),
        Name = "Функциональное тестирование",
        GroupId = ManualTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill RegressionTestingSkill = new()
    {
        Id = Guid.Parse("60000000-0000-0000-0000-000000000002"),
        Name = "Регрессионное тестирование",
        GroupId = ManualTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ExploratoryTestingSkill = new()
    {
        Id = Guid.Parse("60000000-0000-0000-0000-000000000003"),
        Name = "Исследовательское тестирование",
        GroupId = ManualTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill UiUxTestingSkill = new()
    {
        Id = Guid.Parse("60000000-0000-0000-0000-000000000004"),
        Name = "UI/UX тестирование",
        GroupId = ManualTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill MobileTestingSkill = new()
    {
        Id = Guid.Parse("60000000-0000-0000-0000-000000000005"),
        Name = "Мобильное тестирование",
        GroupId = ManualTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    // === AUTO TESTING SKILLS ===
    private static readonly Skill SeleniumSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000001"),
        Name = "Selenium",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill CypressSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000002"),
        Name = "Cypress",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PlaywrightSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000003"),
        Name = "Playwright",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AppiumSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000004"),
        Name = "Appium",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JUnitSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000005"),
        Name = "JUnit",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NUnitSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000006"),
        Name = "NUnit",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill XUnitSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000007"),
        Name = "xUnit",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JestSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000008"),
        Name = "Jest",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PytestSkill = new()
    {
        Id = Guid.Parse("60100000-0000-0000-0000-000000000009"),
        Name = "Pytest",
        GroupId = AutoTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JMeterSkill = new()
    {
        Id = Guid.Parse("60200000-0000-0000-0000-000000000001"),
        Name = "JMeter",
        GroupId = PerformanceTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GatlingSkill = new()
    {
        Id = Guid.Parse("60200000-0000-0000-0000-000000000002"),
        Name = "Gatling",
        GroupId = PerformanceTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill K6Skill = new()
    {
        Id = Guid.Parse("60200000-0000-0000-0000-000000000003"),
        Name = "k6",
        GroupId = PerformanceTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill LocustSkill = new()
    {
        Id = Guid.Parse("60200000-0000-0000-0000-000000000004"),
        Name = "Locust",
        GroupId = PerformanceTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill OwaspSkill = new()
    {
        Id = Guid.Parse("60300000-0000-0000-0000-000000000001"),
        Name = "OWASP",
        GroupId = SecurityTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PenetrationTestingSkill = new()
    {
        Id = Guid.Parse("60300000-0000-0000-0000-000000000002"),
        Name = "Penetration Testing",
        GroupId = SecurityTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill BurpSuiteSkill = new()
    {
        Id = Guid.Parse("60300000-0000-0000-0000-000000000003"),
        Name = "Burp Suite",
        GroupId = SecurityTestingGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JenkinsSkill = new()
    {
        Id = Guid.Parse("70000000-0000-0000-0000-000000000001"),
        Name = "Jenkins",
        GroupId = CICDGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GitLabCISkill = new()
    {
        Id = Guid.Parse("70000000-0000-0000-0000-000000000002"),
        Name = "GitLab CI/CD",
        GroupId = CICDGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GitHubActionsSkill = new()
    {
        Id = Guid.Parse("70000000-0000-0000-0000-000000000003"),
        Name = "GitHub Actions",
        GroupId = CICDGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AzurePipelinesSkill = new()
    {
        Id = Guid.Parse("70000000-0000-0000-0000-000000000004"),
        Name = "Azure Pipelines",
        GroupId = CICDGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill TeamCitySkill = new()
    {
        Id = Guid.Parse("70000000-0000-0000-0000-000000000005"),
        Name = "TeamCity",
        GroupId = CICDGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ArgoCDSkill = new()
    {
        Id = Guid.Parse("70000000-0000-0000-0000-000000000006"),
        Name = "ArgoCD",
        GroupId = CICDGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AwsSkill = new()
    {
        Id = Guid.Parse("70100000-0000-0000-0000-000000000001"),
        Name = "AWS",
        GroupId = CloudGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AzureSkill = new()
    {
        Id = Guid.Parse("70100000-0000-0000-0000-000000000002"),
        Name = "Microsoft Azure",
        GroupId = CloudGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GcpSkill = new()
    {
        Id = Guid.Parse("70100000-0000-0000-0000-000000000003"),
        Name = "Google Cloud Platform",
        GroupId = CloudGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill YandexCloudSkill = new()
    {
        Id = Guid.Parse("70100000-0000-0000-0000-000000000004"),
        Name = "Yandex Cloud",
        GroupId = CloudGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill DigitalOceanSkill = new()
    {
        Id = Guid.Parse("70100000-0000-0000-0000-000000000005"),
        Name = "DigitalOcean",
        GroupId = CloudGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill DockerSkill = new()
    {
        Id = Guid.Parse("70200000-0000-0000-0000-000000000001"),
        Name = "Docker",
        GroupId = ContainerizationGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill KubernetesSkill = new()
    {
        Id = Guid.Parse("70200000-0000-0000-0000-000000000002"),
        Name = "Kubernetes",
        GroupId = ContainerizationGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill HelmSkill = new()
    {
        Id = Guid.Parse("70200000-0000-0000-0000-000000000003"),
        Name = "Helm",
        GroupId = ContainerizationGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill DockerComposeSkill = new()
    {
        Id = Guid.Parse("70200000-0000-0000-0000-000000000004"),
        Name = "Docker Compose",
        GroupId = ContainerizationGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PrometheusSkill = new()
    {
        Id = Guid.Parse("70300000-0000-0000-0000-000000000001"),
        Name = "Prometheus",
        GroupId = MonitoringGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill GrafanaSkill = new()
    {
        Id = Guid.Parse("70300000-0000-0000-0000-000000000002"),
        Name = "Grafana",
        GroupId = MonitoringGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ElkSkill = new()
    {
        Id = Guid.Parse("70300000-0000-0000-0000-000000000003"),
        Name = "ELK Stack",
        GroupId = MonitoringGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill DatadogSkill = new()
    {
        Id = Guid.Parse("70300000-0000-0000-0000-000000000004"),
        Name = "Datadog",
        GroupId = MonitoringGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill ZabbixSkill = new()
    {
        Id = Guid.Parse("70300000-0000-0000-0000-000000000005"),
        Name = "Zabbix",
        GroupId = MonitoringGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill JaegerSkill = new()
    {
        Id = Guid.Parse("70300000-0000-0000-0000-000000000006"),
        Name = "Jaeger",
        GroupId = MonitoringGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill TerraformSkill = new()
    {
        Id = Guid.Parse("70400000-0000-0000-0000-000000000001"),
        Name = "Terraform",
        GroupId = InfrastructureGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill AnsibleSkill = new()
    {
        Id = Guid.Parse("70400000-0000-0000-0000-000000000002"),
        Name = "Ansible",
        GroupId = InfrastructureGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill PulumiSkill = new()
    {
        Id = Guid.Parse("70400000-0000-0000-0000-000000000003"),
        Name = "Pulumi",
        GroupId = InfrastructureGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill LinuxSkill = new()
    {
        Id = Guid.Parse("70400000-0000-0000-0000-000000000004"),
        Name = "Linux Administration",
        GroupId = InfrastructureGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill NginxSkill = new()
    {
        Id = Guid.Parse("70400000-0000-0000-0000-000000000005"),
        Name = "Nginx",
        GroupId = InfrastructureGroup.Id,
        CreatedUtc = _utc_date
    };

    private static readonly Skill HAProxySkill = new()
    {
        Id = Guid.Parse("70400000-0000-0000-0000-000000000006"),
        Name = "HAProxy",
        GroupId = InfrastructureGroup.Id,
        CreatedUtc = _utc_date
    };

    public static List<Skill> GetAllSkills() => new()
    {
        // .NET
        CSharpSkill, AspNetCoreSkill, EntityFrameworkSkill, BlazorSkill, WpfSkill, XamarinSkill, MauiSkill, LinqSkill,
            
        // Java
        JavaCoreSkill, SpringFrameworkSkill, SpringBootSkill, HibernateSkill, KotlinSkill, MavenSkill, GradleSkill,
            
        // Python
        PythonCoreSkill, DjangoSkill, FastApiSkill, FlaskSkill, PandasSkill, NumPySkill,
            
        // Node.js
        NodeJsCoreSkill, ExpressSkill, NestJsSkill, NextJsSkill,
            
        // Go
        GoCoreSkill, GinSkill,
            
        // PHP
        PhpCoreSkill, LaravelSkill, SymfonySkill, YiiSkill,
            
        // Ruby
        RubyCoreSkill, RailsSkill,
            
        // C/C++
        CppSkill, CSkill, QtSkill,
        // Rust
        RustCoreSkill, TokioSkill,
            
        // React
        ReactCoreSkill, ReduxSkill, ReactRouterSkill, ReactQuerySkill, NextJsFrontSkill,
            
        // Angular
        AngularCoreSkill, RxJsSkill, NgRxSkill,
        // Vue
        VueCoreSkill, VuexSkill, PiniaSkill, NuxtSkill,
            
        // JavaScript
        JsCoreSkill, Es6Skill, WebpackSkill, ViteSkill,
            
        // TypeScript
        TsCoreSkill,
            
        // CSS
        CssCoreSkill, SassSkill, TailwindSkill, BootstrapSkill, StyledComponentsSkill,
            
        // iOS
        SwiftSkill, SwiftUI, UIKitSkill, ObjectiveCSkill,
            
        // Android
        AndroidKotlinSkill, JetpackComposeSkill, AndroidXmlSkill,
            
        // Cross-platform
        FlutterSkill, DartSkill, ReactNativeSkill,
            
        // System Analytics
        UmlSkill, BpmnSkill, SqlAnalyticsSkill, ApiDesignSkill, SwaggerSkill, ArchimateSkill,
            
        // Business Analytics
        RequirementsGatheringSkill, UserStoriesSkill, ProcessModelingSkill, BusinessProcessSkill, CompetitorsAnalysisSkill,
        // Data Analytics
        PowerBISkill, TableauSkill, ExcelAnalyticsSkill, PythonDataSkill, MachineLearningSkill,
        // Product Management
        ProductStrategySkill, RoadmapSkill, ScrumSkill, KanbanSkill,
            
        // Manual Testing
        FunctionalTestingSkill, RegressionTestingSkill, ExploratoryTestingSkill, UiUxTestingSkill, MobileTestingSkill,
            
        // Auto Testing
        SeleniumSkill, CypressSkill, PlaywrightSkill, AppiumSkill, JUnitSkill, NUnitSkill, XUnitSkill, JestSkill, PytestSkill,
            
        // Performance Testing
        JMeterSkill, GatlingSkill, K6Skill, LocustSkill,
        // Security Testing
        OwaspSkill, PenetrationTestingSkill, BurpSuiteSkill,
            
        // CI/CD
        JenkinsSkill, GitLabCISkill, GitHubActionsSkill, AzurePipelinesSkill, TeamCitySkill, ArgoCDSkill,
            
        // Cloud
        AwsSkill, AzureSkill, GcpSkill, YandexCloudSkill, DigitalOceanSkill,
            
        // Containerization
        DockerSkill, KubernetesSkill, HelmSkill, DockerComposeSkill,
            
        // Monitoring
        PrometheusSkill, GrafanaSkill, ElkSkill, DatadogSkill, ZabbixSkill, JaegerSkill,

        // Infrastructure
        TerraformSkill, AnsibleSkill, PulumiSkill, LinuxSkill, NginxSkill, HAProxySkill
    };
}