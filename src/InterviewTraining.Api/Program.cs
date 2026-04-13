using InterviewTraining.Api;
using InterviewTraining.Api.Middlewares;
using InterviewTraining.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;

var builder = WebApplication
    .CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
builder.Configuration.AddJsonFile("secrets/appsettings.secrets.json", optional: true);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

builder.Services
    .AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Debug);
    });

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    using var context = scope.ServiceProvider.GetRequiredService<InterviewContext>();
    // Применяем миграции
    var pendingMigrations = context.Database.GetPendingMigrations();
    if (pendingMigrations != null && pendingMigrations.Any())
    {
        context.Database.Migrate();
    }
}

await InterviewContextSeeding.SeedAllAsync(app.Services);

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

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

app.Run();