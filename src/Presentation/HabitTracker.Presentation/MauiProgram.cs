using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Services.Notification;
using JFomit.Functional.Monads;
using Microsoft.Extensions.Logging;
namespace HabitTracker.Presentation
{
    public class DummyHabitReminderRepository : IHabitReminderRepository
    {
        public IQueryable<HabitReminderEntity> Habits => Enumerable.Empty<HabitReminderEntity>().AsQueryable();

        public ICollection<HabitReminderEntity> GetAll() => new List<HabitReminderEntity>();

        public Result<int, string> AddHabit(HabitReminderEntity entity) => Result<int, string>.Ok(0);

        public Result<HabitReminderEntity, string> DeleteHabit(int id) => Result<HabitReminderEntity, string>.Fail("Not implemented");

        public Result<HabitReminderEntity, string> UpdateHabit(HabitReminderEntity entity, Action<HabitReminderEntity> action)
            => Result<HabitReminderEntity, string>.Fail("Not implemented");
    }
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddSingleton<IHabitReminderRepository, DummyHabitReminderRepository>();
            builder.Services.AddSingleton<AndroidNotificationService>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
