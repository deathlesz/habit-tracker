using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Domain.Entities;
using static JFomit.Functional.Prelude;
using JFomit.Functional.Monads;
using HabitTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Platforms.Android.Repositories
{
	public class EfHabitRepository : IHabitRepository
	{
		private readonly AppDbContext _db;
		public EfHabitRepository(AppDbContext db) => _db = db;
		public IQueryable<HabitEntity> Habits => _db.Habits
		.Include(h => h.Regularity)
		.Include(h => h.Reminder)
		.AsQueryable();
		public ICollection<HabitEntity> GetAll() => Habits.ToList();
		public Result<int, string> AddHabit(HabitEntity entity)
		{
			try
			{
				_db.Habits.Add(entity);
				_db.SaveChanges();
				return Result<int, string>.Ok(entity.Id);
			}
			catch (Exception ex)
			{
				return Result<int, string>.Fail(ex.Message);
			}
		}

		public Result<HabitEntity, string> DeleteHabit(int id)
		{
			var entity = _db.Habits
			.Include(h => h.Regularity)
			.Include(h => h.Reminder)
			.FirstOrDefault(h => h.Id == id);
			if (entity == null)
				return Result<HabitEntity, string>.Fail($"Habit {id} not found");
			try
			{
				_db.Remove(entity);
				_db.SaveChanges();
				return Ok(entity);
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
		}


		public Result<HabitEntity, string> UpdateHabit(HabitEntity habitEntity, Action<HabitEntity> action)
		{
			try
			{
				var entry = _db.Habits
				.Include(h => h.Regularity)
				.Include(h => h.Reminder)
				.FirstOrDefault(h => h.Id == habitEntity.Id);
				if (entry == null)
					return Error($"Habit {habitEntity.Id} not found");
				action(entry);
				_db.SaveChanges();
				return Ok(entry);
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
		}
	}
}