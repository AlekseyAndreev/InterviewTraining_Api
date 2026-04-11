using System;
using InterviewTraining.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InterviewTraining.Infrastructure.DatabaseContext;

/// <summary>
/// Фабрика создания миграций
/// </summary>
public class InterviewContextFactory : IDesignTimeDbContextFactory<InterviewContext>
{
    public InterviewContext CreateDbContext(string[] args)
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<InterviewContext>();
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();

        var connectionString = ConfigHelper.GetSettingFromConfig(configuration, "ConnectionStrings", "InterviewTrainingConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Configuration setting does not exist.Setting name ConnectionStrings:InterviewTrainingConnection");
        }
        dbContextOptionsBuilder.UseNpgsql(connectionString, opt => opt.MigrationsAssembly("InterviewTraining.Infrastructure"));
        Console.WriteLine($"connectionString - {connectionString}");
        return new InterviewContext(dbContextOptionsBuilder.Options);
    }
}
