using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure;
using JFomit.Functional.Monads; 
using System;
using static JFomit.Functional.Prelude;

namespace HabitTracker.Infrastructure.Platforms.Android.Repositories
{
	public class EfHabitReminderRepository : IHabitReminderRepository
	{
		private readonly AppDbContext _db;
		public EfHabitReminderRepository(AppDbContext db) => _db = db;
		public IQueryable<HabitReminderEntity> Habits => _db.HabitReminders.AsQueryable();
		public ICollection<HabitReminderEntity> GetAll() => Habits.ToList();
		public Result<int, string> AddHabit(HabitReminderEntity entity)
		{
			try
			{
				_db.HabitReminders.Add(entity);
				_db.SaveChanges();
				return Result<int, string>.Ok(entity.Id);
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
		}


		public Result<HabitReminderEntity, string> DeleteHabit(int id)
		{
			var entity = _db.HabitReminders.FirstOrDefault(r => r.Id == id);
			if (entity == null)
				return Error($"Reminder {id} not found");
			try
			{
				_db.HabitReminders.Remove(entity);
				_db.SaveChanges();
				return Result<HabitReminderEntity, string>.Ok(entity);
			}
			catch (Exception ex)
			{
				return Result<HabitReminderEntity, string>.Fail(ex.Message);
			}
		}


		public Result<HabitReminderEntity, string> UpdateHabit(HabitReminderEntity habitReminderEntity, Action<HabitReminderEntity> action)
		{
			try
			{
				var tracked = _db.HabitReminders.FirstOrDefault(r => r.Id == habitReminderEntity.Id);
				if (tracked == null)
					return Result<HabitReminderEntity, string>.Fail($"Reminder {habitReminderEntity.Id} not found");
				action(tracked);
				_db.SaveChanges();
				return Result<HabitReminderEntity, string>.Ok(tracked);
			}
			catch (Exception ex)
			{
				return Result<HabitReminderEntity, string>.Fail(ex.Message);
			}
		}
	}
}