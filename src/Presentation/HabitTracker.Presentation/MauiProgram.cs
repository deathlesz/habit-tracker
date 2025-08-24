using Microsoft.Extensions.Logging;
using HabitTracker.Infrastructure.Platforms.Android.ProgramConfiguration;
using HabitTracker.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using HabitTracker.Presentation.ProgramConfiguration;
namespace HabitTracker.Presentation
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddInfrastructureLayer();
            builder.Services.AddPresentationLayer();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var application = builder.Build();
            application.UseInfrastructureLayerSystems();
            return application;
        }
    }

}
