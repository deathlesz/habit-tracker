using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Domain.Entities
{
    public class HabitReminderEntity
    {
        /// <summary>
        /// Will be automatically set by repository
        /// </summary>
        public int Id { get; set; }
        public DateOnly StartDate {  get; set; }
        public int CyclePatternLength { get; set; }
        public ICollection<int> DaysToNotificate { get; set; }
        public int? CyclesToRun {  get; set; }
        public TimeOnly Time { get; set; }
        public string Message { get; set; }
    }

}
