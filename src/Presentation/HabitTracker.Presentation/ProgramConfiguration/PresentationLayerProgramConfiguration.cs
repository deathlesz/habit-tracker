using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Presentation.ProgramConfiguration
{
    public static class PresentationLayerProgramConfiguration
    {
        /// <summary>
        /// To use DI in pages, they has to be added to services container.
        /// All added pages should be added to services here
        /// </summary>
        /// <param name="services"></param>
        public static void AddPresentationLayer(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();

        }
    }
}
