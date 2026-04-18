using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    private static readonly DateTime _utc_date = new DateTime(2026, 11, 04, 11, 24, 00, DateTimeKind.Utc);

    public static async Task SeedAllAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InterviewContext>();
        await AddSkillsAndRelated(context);
        await AddTimeZones(context);
        await AddInterviewLanguages(context);
        await AddCurrencies(context);
    }

    private static async Task AddSkillsAndRelated(InterviewContext context)
    {
        if (await context.SkillGroups.AnyAsync())
        {
            return;
        }

        await context.SkillGroups.AddRangeAsync(GetAllGroups());

        await context.Skills.AddRangeAsync(GetAllSkills());

        await context.SkillTags.AddRangeAsync(AllTags);

        await context.SaveChangesAsync();
    }

    private static async Task AddTimeZones(InterviewContext context)
    {
        if (await context.TimeZones.AnyAsync())
        {
            return;
        }

        await context.TimeZones.AddRangeAsync(GetAllTimeZones());

        await context.SaveChangesAsync();
    }

    private static async Task AddInterviewLanguages(InterviewContext context)
    {
        if (await context.InterviewLanguages.AnyAsync())
        {
            return;
        }

        await context.InterviewLanguages.AddRangeAsync(GetInterviewLanguages());

        await context.SaveChangesAsync();
    }

    private static async Task AddCurrencies(InterviewContext context)
    {
        if (await context.Currencies.AnyAsync())
        {
            return;
        }

        await context.Currencies.AddRangeAsync(GetCurrencies());

        await context.SaveChangesAsync();
    }
}
