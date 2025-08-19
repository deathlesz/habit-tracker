using HabitTracker.Domain.Entities;
using JFomit.Functional.Monads;

namespace HabitTracker.Application.Interfaces.Repositories
{
    public interface IHabitRepository
    {
        IQueryable<HabitEntity> Habits { get; }
        public ICollection<HabitEntity> GetAll();
        public Result<int, string> AddHabit(HabitEntity entity);
        public Result<HabitEntity, string> DeleteHabit(int id);
		public Result<HabitEntity, string> UpdateHabit(HabitEntity habitEntity, Action<HabitEntity> action);
	}
}
