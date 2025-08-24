using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Infrastructure.Platforms.Android.Repositories;
using Microsoft.EntityFrameworkCore;


namespace HabitTracker.Infrastructure.Platforms.Android.ProgramConfiguration
{
    /// <summary>
    /// Extension class, used to configure all infrastructure layer services in MauiProgram.cs file.
    /// </summary>
    public static class AndroidInfrastructureLayerExtension
    {
        /// <summary>
        /// Center configurating extension of Infrastructure layer
        /// In MauiProgram.cs file use app.Services.AddInfrastructureLayer() to add all services from HabitTracker.Infrastructure
        /// </summary>

        //TODO in future we will be able configurate it with smth like appsettings.json, but not now
        public static string Filename = Path.Combine(FileSystem.AppDataDirectory, "habittracker_migrations.db");
        public static string ConnectionString = $"Filename={Filename}";
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlite(ConnectionString)); // TODO Перенести в конфиг файлы
            services.AddSingleton<IHabitRepository, EfHabitRepository>();
            services.AddSingleton<IHabitReminderRepository, EfHabitReminderRepository>();
            //services.AddSingleton<INotificationService, {SERVICE_IMPLEMENTATION_NAME}>();
        }

        public static void UseInfrastructureLayerSystems(this MauiApp app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }
        }


    }
}
