using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Domain.Entities.Regularity
{
    /// <summary>
    /// An entity that contains information about a habit's SCHEDULE.
    /// </summary>
    public class HabitScheduleEntity
    {
        /// <summary>
        /// Will be automatically set by repository
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Record the length of a repeating habit cycle. 
        /// Even if a habit is repeated 1 time (approx. 1 week), it should also be considered (1 repetition)
        /// </summary>
        public int RepeatingCycleDays { get; set; } // Для еженедельных целей - 7, ежедневных - 1...
        /// <summary>
        /// A label indicating that the habit can be performed on any day.
        /// In this case, the RepeatingDatesToMatch and AddictionalDatesToMatch fields are not required
        /// </summary>
        public bool IsAnyDay { get; set; }
        /// <summary>
        /// A label indicating that the habit can be performed on any day in the schedule (not on all days).
        /// Logically, it is mutually exclusive with IsAnyDay.
        /// </summary>
        public bool IsAllMachedDays { get; set; }
        /// <summary>
        /// The number of completed days in the cycle for completing a task.
        /// </summary>
        public int CycleMachedDaysGoal { get; set; }

        /// <summary>
        /// The start of the cycle. The count of days and cycles in the habit starts from this day. 
        /// Example - a habit created on Wednesday with a repeat record on Monday, Tuesday, and Thursday.
        /// In this case, StartDate is Monday of the same week, and RepeatingDatesToMatch is 0, 1, and 3
        /// </summary>
        public DateOnly? StartDate { get; set; }
        /// <summary>
        /// In a recurring habit, which days from the beginning should be repeated in each cycle
        /// </summary>
        public ICollection<int>? RepeatingDatesToMatch { get; set; }

        /// <summary>
        /// A field for storing the days when the habit was successfully marked
        /// </summary>
        public required ICollection<DateOnly> DatesMatched { get; set; }
        /// <summary>
        /// The number of marked days per cycle.
        /// </summary>
        public int DaysMachedInCycle { get; set; }
    }
}
