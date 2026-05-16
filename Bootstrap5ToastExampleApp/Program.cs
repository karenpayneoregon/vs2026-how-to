using Bootstrap5ToastExampleApp.Classes;
using Bootstrap5ToastExampleApp.Models;

namespace Bootstrap5ToastExampleApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        // Configure ToastOptions using the appsettings.json configuration section  
        builder.Services.Configure<ToastOptions>(
            builder.Configuration.GetSection(nameof(ToastOptions)));

        // Register the ReadToastConfiguration service for dependency injection
        builder.Services.AddScoped<ReadToastConfiguration>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
