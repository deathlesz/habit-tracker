using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Domain.Entities.Regularity
{
    /// <summary>
    /// Сущность содержащая информацию о РАСПИСАНИИ привычки. 
    /// </summary>
    public class HabitScheduleEntity
    {
        /// <summary>
        /// Запись длины цикла повторяющейся привычки. 
        /// Даже если привычка повторяется 1 раз (прим. 1 неделю), рассматривать ее стоит также (1 повторение)
        /// </summary>
        public int RepeatingCycleDays { get; set; } // Для еженедельных целей - 7, ежедневных - 1...
        /// <summary>
        /// Метка о том что привычку можно выполнять в любой день.
        /// Тогда поля RepeatingDatesToMatch и AddictionalDatesToMatch не требуются
        /// </summary>
        public bool IsAnyDay { get; set; }
        /// <summary>
        /// Метка о том что привычку можно выполнять в любой из дней в расписании (а не во все).
        /// Логически, взаимоисключающие с IsAnyDay.
        /// </summary>
        public bool IsAllMachedDays { get; set; }
        /// <summary>
        /// Количество выполненных дней в цикле для выполнения таски.
        /// </summary>
        public int CycleMachedDaysGoal { get; set; } 

        /// <summary>
        /// Начало отсчета цикла. Отсчет дней и циклов в привычке идет от этого дня. 
        /// Пример - привычка, создана в среду с записью на повторение в пн, вт, чт.
        /// Тогда StartDate - Понедельник той же недели, а в RepeatingDatesToMatch - 0, 1, 3
        /// </summary>
        public DateOnly? StartDate { get; set; }
        /// <summary>
        /// При повторяющейся привычке, какие дни от начала должны повторятся на каждом цикле
        /// </summary>
        public ICollection<int> RepeatingDatesToMatch { get; set; }

        /// <summary>
        /// Поле для хранения дней, в которые привычка была успешно отмечена
        /// </summary>
        public ICollection<DateOnly> DatesMatched { get; set; }
        /// <summary>
        /// Количество отмеченных дней за цикл. 
        /// </summary>
        public int DaysMachedInCycle { get; set; } 
    }
}
