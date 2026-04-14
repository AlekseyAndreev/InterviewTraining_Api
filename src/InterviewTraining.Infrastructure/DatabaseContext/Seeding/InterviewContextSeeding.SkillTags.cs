using InterviewTraining.Domain;
using System;
using System.Collections.Generic;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    public static readonly List<SkillTag> AllTags = new()
    {
        // C# tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000001"), SkillId = CSharpSkill.Id, Name = "c#", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000002"), SkillId = CSharpSkill.Id, Name = "csharp", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000003"), SkillId = CSharpSkill.Id, Name = "си шарп", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000004"), SkillId = CSharpSkill.Id, Name = "сишарп", CreatedUtc = _utc_date },

        // .NET tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000005"), SkillId = AspNetCoreSkill.Id, Name = "aspnet", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000006"), SkillId = AspNetCoreSkill.Id, Name = "asp net", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000007"), SkillId = AspNetCoreSkill.Id, Name = "aspnetcore", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000008"), SkillId = AspNetCoreSkill.Id, Name = "dotnet", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000009"), SkillId = AspNetCoreSkill.Id, Name = ".net", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000010"), SkillId = AspNetCoreSkill.Id, Name = "дотнет", CreatedUtc = _utc_date },

        // Entity Framework tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000011"), SkillId = EntityFrameworkSkill.Id, Name = "ef", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000012"), SkillId = EntityFrameworkSkill.Id, Name = "ef core", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000013"), SkillId = EntityFrameworkSkill.Id, Name = "entityframework", CreatedUtc = _utc_date },

        // Blazor tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000014"), SkillId = BlazorSkill.Id, Name = "blazor", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000015"), SkillId = BlazorSkill.Id, Name = "blazor server", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000016"), SkillId = BlazorSkill.Id, Name = "blazor wasm", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000017"), SkillId = BlazorSkill.Id, Name = "blazor webassembly", CreatedUtc = _utc_date },

        // WPF tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000018"), SkillId = WpfSkill.Id, Name = "wpf", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000019"), SkillId = WpfSkill.Id, Name = "windows presentation foundation", CreatedUtc = _utc_date },

        // Java tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000020"), SkillId = JavaCoreSkill.Id, Name = "java", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000021"), SkillId = JavaCoreSkill.Id, Name = "джава", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000022"), SkillId = JavaCoreSkill.Id, Name = "ява", CreatedUtc = _utc_date },

        // Spring tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000023"), SkillId = SpringFrameworkSkill.Id, Name = "spring", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000024"), SkillId = SpringFrameworkSkill.Id, Name = "spring framework", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000025"), SkillId = SpringBootSkill.Id, Name = "springboot", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000026"), SkillId = SpringBootSkill.Id, Name = "spring boot", CreatedUtc = _utc_date },

        // Kotlin tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000027"), SkillId = KotlinSkill.Id, Name = "kotlin", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000028"), SkillId = KotlinSkill.Id, Name = "котлин", CreatedUtc = _utc_date },

        // Python tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000029"), SkillId = PythonCoreSkill.Id, Name = "python", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000030"), SkillId = PythonCoreSkill.Id, Name = "питон", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000031"), SkillId = PythonCoreSkill.Id, Name = "py", CreatedUtc = _utc_date },

        // Django tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000032"), SkillId = DjangoSkill.Id, Name = "django", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000033"), SkillId = DjangoSkill.Id, Name = "джанго", CreatedUtc = _utc_date },

        // FastAPI tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000034"), SkillId = FastApiSkill.Id, Name = "fastapi", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000035"), SkillId = FastApiSkill.Id, Name = "fast api", CreatedUtc = _utc_date },

        // Node.js tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000036"), SkillId = NodeJsCoreSkill.Id, Name = "nodejs", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000037"), SkillId = NodeJsCoreSkill.Id, Name = "node js", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000038"), SkillId = NodeJsCoreSkill.Id, Name = "node", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000039"), SkillId = NodeJsCoreSkill.Id, Name = "нода", CreatedUtc = _utc_date },

        // Express tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000040"), SkillId = ExpressSkill.Id, Name = "express", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000041"), SkillId = ExpressSkill.Id, Name = "expressjs", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000042"), SkillId = ExpressSkill.Id, Name = "express.js", CreatedUtc = _utc_date },

        // NestJS tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000043"), SkillId = NestJsSkill.Id, Name = "nestjs", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000044"), SkillId = NestJsSkill.Id, Name = "nest js", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000045"), SkillId = NestJsSkill.Id, Name = "nest", CreatedUtc = _utc_date },

        // Go tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000046"), SkillId = GoCoreSkill.Id, Name = "go", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000047"), SkillId = GoCoreSkill.Id, Name = "golang", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000048"), SkillId = GoCoreSkill.Id, Name = "голанг", CreatedUtc = _utc_date },

        // PHP tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000049"), SkillId = PhpCoreSkill.Id, Name = "php", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000050"), SkillId = PhpCoreSkill.Id, Name = "пхп", CreatedUtc = _utc_date },

        // Laravel tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000051"), SkillId = LaravelSkill.Id, Name = "laravel", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000052"), SkillId = LaravelSkill.Id, Name = "ларавель", CreatedUtc = _utc_date },

        // Ruby tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000053"), SkillId = RubyCoreSkill.Id, Name = "ruby", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000054"), SkillId = RubyCoreSkill.Id, Name = "руби", CreatedUtc = _utc_date },

        // Rails tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000055"), SkillId = RailsSkill.Id, Name = "rails", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000056"), SkillId = RailsSkill.Id, Name = "ror", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000057"), SkillId = RailsSkill.Id, Name = "ruby on rails", CreatedUtc = _utc_date },

        // C++ tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000058"), SkillId = CppSkill.Id, Name = "c++", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000059"), SkillId = CppSkill.Id, Name = "cpp", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000060"), SkillId = CppSkill.Id, Name = "си плюс плюс", CreatedUtc = _utc_date },

        // Rust tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000061"), SkillId = RustCoreSkill.Id, Name = "rust", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000062"), SkillId = RustCoreSkill.Id, Name = "раст", CreatedUtc = _utc_date },

        // React tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000063"), SkillId = ReactCoreSkill.Id, Name = "react", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000064"), SkillId = ReactCoreSkill.Id, Name = "reactjs", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000065"), SkillId = ReactCoreSkill.Id, Name = "react.js", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000066"), SkillId = ReactCoreSkill.Id, Name = "реакт", CreatedUtc = _utc_date },

        // Redux tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000067"), SkillId = ReduxSkill.Id, Name = "redux", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000068"), SkillId = ReduxSkill.Id, Name = "редакс", CreatedUtc = _utc_date },

        // Next.js tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000069"), SkillId = NextJsFrontSkill.Id, Name = "nextjs", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000070"), SkillId = NextJsFrontSkill.Id, Name = "next js", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000071"), SkillId = NextJsFrontSkill.Id, Name = "некст", CreatedUtc = _utc_date },

        // Angular tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000072"), SkillId = AngularCoreSkill.Id, Name = "angular", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000073"), SkillId = AngularCoreSkill.Id, Name = "ангуляр", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000074"), SkillId = AngularCoreSkill.Id, Name = "angularjs", CreatedUtc = _utc_date },

        // RxJS tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000075"), SkillId = RxJsSkill.Id, Name = "rxjs", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000076"), SkillId = RxJsSkill.Id, Name = "rx js", CreatedUtc = _utc_date },

        // Vue tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000077"), SkillId = VueCoreSkill.Id, Name = "vue", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000078"), SkillId = VueCoreSkill.Id, Name = "vuejs", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000079"), SkillId = VueCoreSkill.Id, Name = "vue.js", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000080"), SkillId = VueCoreSkill.Id, Name = "вью", CreatedUtc = _utc_date },

        // Nuxt tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000081"), SkillId = NuxtSkill.Id, Name = "nuxt", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000082"), SkillId = NuxtSkill.Id, Name = "nuxtjs", CreatedUtc = _utc_date },

        // JavaScript tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000083"), SkillId = JsCoreSkill.Id, Name = "javascript", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000084"), SkillId = JsCoreSkill.Id, Name = "js", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000085"), SkillId = JsCoreSkill.Id, Name = "джаваскрипт", CreatedUtc = _utc_date },

        // TypeScript tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000086"), SkillId = TsCoreSkill.Id, Name = "typescript", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000087"), SkillId = TsCoreSkill.Id, Name = "ts", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000088"), SkillId = TsCoreSkill.Id, Name = "тайпскрипт", CreatedUtc = _utc_date },

        // CSS tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000089"), SkillId = CssCoreSkill.Id, Name = "css", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000090"), SkillId = CssCoreSkill.Id, Name = "css3", CreatedUtc = _utc_date },

        // Sass tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000091"), SkillId = SassSkill.Id, Name = "sass", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000092"), SkillId = SassSkill.Id, Name = "scss", CreatedUtc = _utc_date },

        // Tailwind tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000093"), SkillId = TailwindSkill.Id, Name = "tailwind", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000094"), SkillId = TailwindSkill.Id, Name = "tailwindcss", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000095"), SkillId = TailwindSkill.Id, Name = "tailwind css", CreatedUtc = _utc_date },

        // Bootstrap tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000096"), SkillId = BootstrapSkill.Id, Name = "bootstrap", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000097"), SkillId = BootstrapSkill.Id, Name = "бутстрап", CreatedUtc = _utc_date },

        // Swift tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000098"), SkillId = SwiftSkill.Id, Name = "swift", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000099"), SkillId = SwiftSkill.Id, Name = "свифт", CreatedUtc = _utc_date },

        // SwiftUI tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000100"), SkillId = SwiftUI.Id, Name = "swiftui", CreatedUtc = _utc_date },

        // Objective-C tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000101"), SkillId = ObjectiveCSkill.Id, Name = "objective-c", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000102"), SkillId = ObjectiveCSkill.Id, Name = "objc", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000103"), SkillId = ObjectiveCSkill.Id, Name = "objective c", CreatedUtc = _utc_date },

        // Android tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000104"), SkillId = AndroidKotlinSkill.Id, Name = "android", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000105"), SkillId = AndroidKotlinSkill.Id, Name = "андроид", CreatedUtc = _utc_date },

        // Jetpack Compose tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000106"), SkillId = JetpackComposeSkill.Id, Name = "jetpack compose", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000107"), SkillId = JetpackComposeSkill.Id, Name = "compose", CreatedUtc = _utc_date },

        // Flutter tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000108"), SkillId = FlutterSkill.Id, Name = "flutter", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000109"), SkillId = FlutterSkill.Id, Name = "флаттер", CreatedUtc = _utc_date },

        // Dart tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000110"), SkillId = DartSkill.Id, Name = "dart", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000111"), SkillId = DartSkill.Id, Name = "дарт", CreatedUtc = _utc_date },

        // React Native tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000112"), SkillId = ReactNativeSkill.Id, Name = "react native", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000113"), SkillId = ReactNativeSkill.Id, Name = "reactnative", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000114"), SkillId = ReactNativeSkill.Id, Name = "rn", CreatedUtc = _utc_date },

        // Power BI tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000115"), SkillId = PowerBISkill.Id, Name = "powerbi", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000116"), SkillId = PowerBISkill.Id, Name = "power bi", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000117"), SkillId = PowerBISkill.Id, Name = "пауэр би", CreatedUtc = _utc_date },

        // Tableau tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000118"), SkillId = TableauSkill.Id, Name = "tableau", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000119"), SkillId = TableauSkill.Id, Name = "табло", CreatedUtc = _utc_date },

        // Machine Learning tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000120"), SkillId = MachineLearningSkill.Id, Name = "ml", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000121"), SkillId = MachineLearningSkill.Id, Name = "machine learning", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000122"), SkillId = MachineLearningSkill.Id, Name = "машинное обучение", CreatedUtc = _utc_date },

        // Selenium tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000123"), SkillId = SeleniumSkill.Id, Name = "selenium", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000124"), SkillId = SeleniumSkill.Id, Name = "селениум", CreatedUtc = _utc_date },

        // Cypress tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000125"), SkillId = CypressSkill.Id, Name = "cypress", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000126"), SkillId = CypressSkill.Id, Name = "кипрес", CreatedUtc = _utc_date },

        // Playwright tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000127"), SkillId = PlaywrightSkill.Id, Name = "playwright", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000128"), SkillId = PlaywrightSkill.Id, Name = "плейрайт", CreatedUtc = _utc_date },

        // Appium tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000129"), SkillId = AppiumSkill.Id, Name = "appium", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000130"), SkillId = AppiumSkill.Id, Name = "аппиум", CreatedUtc = _utc_date },

        // JMeter tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000131"), SkillId = JMeterSkill.Id, Name = "jmeter", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000132"), SkillId = JMeterSkill.Id, Name = "джейметр", CreatedUtc = _utc_date },

        // OWASP tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000133"), SkillId = OwaspSkill.Id, Name = "owasp", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000134"), SkillId = OwaspSkill.Id, Name = "овасп", CreatedUtc = _utc_date },

        // Jenkins tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000135"), SkillId = JenkinsSkill.Id, Name = "jenkins", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000136"), SkillId = JenkinsSkill.Id, Name = "дженкинс", CreatedUtc = _utc_date },

        // GitLab CI tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000137"), SkillId = GitLabCISkill.Id, Name = "gitlab ci", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000138"), SkillId = GitLabCISkill.Id, Name = "gitlab-ci", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000139"), SkillId = GitLabCISkill.Id, Name = "gitlabcicd", CreatedUtc = _utc_date },

        // GitHub Actions tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000140"), SkillId = GitHubActionsSkill.Id, Name = "github actions", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000141"), SkillId = GitHubActionsSkill.Id, Name = "gha", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000142"), SkillId = GitHubActionsSkill.Id, Name = "github-actions", CreatedUtc = _utc_date },

        // AWS tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000143"), SkillId = AwsSkill.Id, Name = "aws", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000144"), SkillId = AwsSkill.Id, Name = "amazon web services", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000145"), SkillId = AwsSkill.Id, Name = "амазон", CreatedUtc = _utc_date },

        // Azure tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000146"), SkillId = AzureSkill.Id, Name = "azure", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000147"), SkillId = AzureSkill.Id, Name = "microsoft azure", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000148"), SkillId = AzureSkill.Id, Name = "азур", CreatedUtc = _utc_date },

        // GCP tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000149"), SkillId = GcpSkill.Id, Name = "gcp", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000150"), SkillId = GcpSkill.Id, Name = "google cloud", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000151"), SkillId = GcpSkill.Id, Name = "gce", CreatedUtc = _utc_date },

        // Docker tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000152"), SkillId = DockerSkill.Id, Name = "docker", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000153"), SkillId = DockerSkill.Id, Name = "докер", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000154"), SkillId = DockerSkill.Id, Name = "container", CreatedUtc = _utc_date },

        // Kubernetes tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000155"), SkillId = KubernetesSkill.Id, Name = "kubernetes", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000156"), SkillId = KubernetesSkill.Id, Name = "k8s", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000157"), SkillId = KubernetesSkill.Id, Name = "кубернетес", CreatedUtc = _utc_date },

        // Helm tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000158"), SkillId = HelmSkill.Id, Name = "helm", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000159"), SkillId = HelmSkill.Id, Name = "хелм", CreatedUtc = _utc_date },

        // Prometheus tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000160"), SkillId = PrometheusSkill.Id, Name = "prometheus", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000161"), SkillId = PrometheusSkill.Id, Name = "прометеус", CreatedUtc = _utc_date },

        // Grafana tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000162"), SkillId = GrafanaSkill.Id, Name = "grafana", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000163"), SkillId = GrafanaSkill.Id, Name = "графана", CreatedUtc = _utc_date },

        // ELK tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000164"), SkillId = ElkSkill.Id, Name = "elk", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000165"), SkillId = ElkSkill.Id, Name = "elasticsearch", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000166"), SkillId = ElkSkill.Id, Name = "kibana", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000167"), SkillId = ElkSkill.Id, Name = "logstash", CreatedUtc = _utc_date },

        // Terraform tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000168"), SkillId = TerraformSkill.Id, Name = "terraform", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000169"), SkillId = TerraformSkill.Id, Name = "tf", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000170"), SkillId = TerraformSkill.Id, Name = "терраформ", CreatedUtc = _utc_date },

        // Ansible tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000171"), SkillId = AnsibleSkill.Id, Name = "ansible", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000172"), SkillId = AnsibleSkill.Id, Name = "ансибл", CreatedUtc = _utc_date },

        // Linux tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000173"), SkillId = LinuxSkill.Id, Name = "linux", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000174"), SkillId = LinuxSkill.Id, Name = "линукс", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000175"), SkillId = LinuxSkill.Id, Name = "ubuntu", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000176"), SkillId = LinuxSkill.Id, Name = "centos", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000177"), SkillId = LinuxSkill.Id, Name = "debian", CreatedUtc = _utc_date },

        // Nginx tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000178"), SkillId = NginxSkill.Id, Name = "nginx", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000179"), SkillId = NginxSkill.Id, Name = "нджинкс", CreatedUtc = _utc_date },

        // Scrum tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000180"), SkillId = ScrumSkill.Id, Name = "scrum", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000181"), SkillId = ScrumSkill.Id, Name = "скрам", CreatedUtc = _utc_date },

        // Kanban tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000182"), SkillId = KanbanSkill.Id, Name = "kanban", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000183"), SkillId = KanbanSkill.Id, Name = "канбан", CreatedUtc = _utc_date },

        // UML tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000184"), SkillId = UmlSkill.Id, Name = "uml", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000185"), SkillId = UmlSkill.Id, Name = "универсальный язык моделирования", CreatedUtc = _utc_date },

        // BPMN tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000186"), SkillId = BpmnSkill.Id, Name = "bpmn", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000187"), SkillId = BpmnSkill.Id, Name = "business process model", CreatedUtc = _utc_date },

        // Swagger tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000188"), SkillId = SwaggerSkill.Id, Name = "swagger", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000189"), SkillId = SwaggerSkill.Id, Name = "openapi", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000190"), SkillId = SwaggerSkill.Id, Name = "open api", CreatedUtc = _utc_date },

        // Jest tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000191"), SkillId = JestSkill.Id, Name = "jest", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000192"), SkillId = JestSkill.Id, Name = "джест", CreatedUtc = _utc_date },

        // Pytest tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000193"), SkillId = PytestSkill.Id, Name = "pytest", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000194"), SkillId = PytestSkill.Id, Name = "пайтест", CreatedUtc = _utc_date },

        // NUnit tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000195"), SkillId = NUnitSkill.Id, Name = "nunit", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000196"), SkillId = NUnitSkill.Id, Name = "н юнит", CreatedUtc = _utc_date },

        // xUnit tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000197"), SkillId = XUnitSkill.Id, Name = "xunit", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000198"), SkillId = XUnitSkill.Id, Name = "x-unit", CreatedUtc = _utc_date },

        // ArgoCD tags
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000199"), SkillId = ArgoCDSkill.Id, Name = "argocd", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000200"), SkillId = ArgoCDSkill.Id, Name = "argo cd", CreatedUtc = _utc_date },
        new() { Id = Guid.Parse("90000000-0000-0000-0000-000000000201"), SkillId = ArgoCDSkill.Id, Name = "арго", CreatedUtc = _utc_date },
    };
}
