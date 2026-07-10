using FluentValidation;
using FluentWebApplication.Classes;
using FluentWebApplication.Data;
using FluentWebApplication.Models;
using FluentWebApplication.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogLibrary;
using System.Diagnostics;
using WebClassLibrary;
using static System.DateTime;

namespace FluentWebApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.AddValidatorsFromAssemblyContaining<PersonValidator>();
        
        builder.SerilogDevelopmentLoggingSetup();

        if (builder.Environment.IsDevelopment())
        {
    
            builder.Services.AddDbContextPool<Context>(options =>
                options.UseSqlServer(builder.Configuration.DefaultConnectionString())
                    .EnableSensitiveDataLogging()
                    .LogTo(new DbContextToFileLogger().Log, [DbLoggerCategory.Database.Command.Name],
                        LogLevel.Information));
        }else
        {
            builder.Services.AddDbContextPool<Context>(options =>
                options.UseSqlServer(builder.Configuration.DefaultConnectionString()));
        }
        
        builder.Services
            .AddOptions<CancellationTokenSettings>()
            .Bind(builder.Configuration.GetSection(CancellationTokenSettings.SectionName))
            .Validate(s => s.Timeout >= 0, "Timeout must be >= 0")
            .ValidateOnStart();

        builder.Services.AddHostedService<EntityCoreWarmupService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
