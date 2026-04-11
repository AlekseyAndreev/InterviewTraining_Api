using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace InterviewTraining.Infrastructure.DatabaseContext;

public static partial class InterviewContextSeeding
{
    private static readonly DateTime _utc_date = new DateTime(2026, 11, 04, 11, 24, 00, DateTimeKind.Utc);

    public static async Task SeedAllSkillsSkillGroupsSkillTagsAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InterviewContext>();

        if (await context.SkillGroups.AnyAsync())
        {
            return;
        }

        await context.SkillGroups.AddRangeAsync(GetAllGroups());

        await context.Skills.AddRangeAsync(GetAllSkills());

        await context.SkillTags.AddRangeAsync(AllTags);

        await context.SaveChangesAsync();
    }
}
