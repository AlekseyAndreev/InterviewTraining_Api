using System;
using System.Data.Common;
using System.Linq;
using InterviewTraining.Infrastructure.DatabaseContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InterviewTraining.Tests;

public class Fixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Выполняется перед запуском тестов
    /// </summary>
    public Fixture()
    {
        var serviceCollection = new ServiceCollection();
        AddDbContextSqlite(serviceCollection);
        // serviceCollection.ConfigureRepositories();
        var serviceProvider = serviceCollection
            .AddLogging()
            .BuildServiceProvider();
        ServiceProvider = serviceProvider;

        ServiceProvider = serviceProvider;
        Environment.SetEnvironmentVariable("SkipDeepLogging", "true");

        // Create a scope to obtain a reference to the database
        using var scope = ServiceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<InterviewContext>();
        var logger = scopedServices
            .GetRequiredService<ILogger<Fixture>>();

        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
        }
    }

    public void Dispose()
    {
    }

    private IServiceCollection AddDbContextSqlite(IServiceCollection services)
    {
        var contextOptions = new DbContextOptionsBuilder<InterviewContext>()
            .UseSqlite(CreateInMemoryDatabase())
            .Options;
        var conn = RelationalOptionsExtension.Extract(contextOptions).Connection;

        services.AddDbContext<InterviewContext>(options =>
        {
            options.UseSqlite(conn);
        });
        services.AddTransient<DbContext, InterviewContext>();
        return services;
    }

    private static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        return connection;
    }
}
