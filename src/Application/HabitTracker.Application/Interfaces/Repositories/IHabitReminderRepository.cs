using HabitTracker.Domain.Entities;
using JFomit.Functional.Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Application.Interfaces.Repositories
{
    public interface IHabitReminderRepository
    {
        IQueryable<HabitReminderEntity> Habits { get; }
        public ICollection<HabitReminderEntity> GetAll();
        public Result<int, string> AddHabit(HabitReminderEntity entity);
        public Result<HabitReminderEntity, string> DeleteHabit(int id);
		public Result<HabitReminderEntity, string> UpdateHabit(HabitReminderEntity habitReminderEntity, Action<HabitReminderEntity> action);
	}
}
