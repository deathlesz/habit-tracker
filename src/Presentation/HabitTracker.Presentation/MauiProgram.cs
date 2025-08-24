using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Application.Interfaces.Services;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Services.Notification;
using JFomit.Functional.Monads;
using Microsoft.Extensions.Logging;
namespace HabitTracker.Presentation
{
    /// <summary>
    /// A dummy implementation of the <see cref="IHabitReminderRepository"/> interface.
    /// This class provides placeholder methods for testing or development purposes without actual data persistence.
    /// </summary>
    public class DummyHabitReminderRepository : IHabitReminderRepository
    {
        public IQueryable<HabitReminderEntity> Habits => Enumerable.Empty<HabitReminderEntity>().AsQueryable();
    
        public ICollection<HabitReminderEntity> GetAll() => new List<HabitReminderEntity>();
    
        public Result<int, string> AddHabit(HabitReminderEntity entity) => Result<int, string>.Ok(0);
    
        public Result<HabitReminderEntity, string> DeleteHabit(int id) => Result<HabitReminderEntity, string>.Fail("Not implemented");
    
        public Result<HabitReminderEntity, string> UpdateHabit(HabitReminderEntity entity, Action<HabitReminderEntity> action)
            => Result<HabitReminderEntity, string>.Fail("Not implemented");
    }
    /// <summary>
    /// Provides configuration and startup logic for the MAUI application.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates and configures the <see cref="MauiApp"/> instance for the application.
        /// </summary>
        /// <returns>
        /// A fully configured <see cref="MauiApp"/> instance.
        /// </returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            //builder.Services.AddSingleton<IHabitReminderRepository, DummyHabitReminderRepository>();
            //builder.Services.AddSingleton<INotificationService>();
            builder.Services.AddSingleton<ReminderPage>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
