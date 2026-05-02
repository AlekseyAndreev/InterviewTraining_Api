using Elastic.Serilog.Sinks;
using InterviewTraining.Api;
using InterviewTraining.Api.Middlewares;
using InterviewTraining.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "InterviewTraining.Api")
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.Elasticsearch()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting InterviewTraining API");

    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    builder.Configuration.AddJsonFile($"appsettings.{System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
    builder.Configuration.AddJsonFile("secrets/appsettings.secrets.json", optional: true);
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddCommandLine(args);

    // Use Serilog instead of built-in logging
    builder.Host.UseSerilog();

    builder.Services.ConfigureServices(builder.Configuration);

    var app = builder.Build();

    using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {
        using var context = scope.ServiceProvider.GetRequiredService<InterviewContext>();
        var pendingMigrations = context.Database.GetPendingMigrations();
        if (pendingMigrations != null && pendingMigrations.Any())
        {
            context.Database.Migrate();
        }
    }

    await InterviewContextSeeding.SeedAllAsync(app.Services);

    var uiUrls = app.Configuration.GetSection("UiUrls").Get<string[]>() ?? Array.Empty<string>();
    app.UseCors(builder => builder
        .WithOrigins(uiUrls)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseRouting();

    app.UseAuthorization();

    app.MapHealthChecks("/health");

    app.MapControllers();

    app.MapHub<InterviewTraining.Infrastructure.SignalR.InterviewChatHub>("/hubs/interview-chat").RequireAuthorization();
    app.MapHub<InterviewTraining.Infrastructure.SignalR.InterviewHub>("/hubs/interview").RequireAuthorization();
    app.MapHub<InterviewTraining.Infrastructure.SignalR.UserWithAdminChatHub>("/hubs/user-with-admin-chat").RequireAuthorization();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
