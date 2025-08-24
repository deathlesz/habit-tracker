using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Entities.Regularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HabitTracker.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<HabitEntity> Habits => Set<HabitEntity>();
    public DbSet<HabitReminderEntity> HabitReminders => Set<HabitReminderEntity>();
    public DbSet<HabitScheduleEntity> HabitSchedules => Set<HabitScheduleEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Общие конвертеры DateOnly/TimeOnly для SQLite
        var dateOnlyToString = new ValueConverter<DateOnly, string>(
        v => v.ToString("yyyy-MM-dd"),
        v => DateOnly.Parse(v));

        var timeOnlyToString = new ValueConverter<TimeOnly, string>(
            v => v.ToString("HH:mm:ss"),
            v => TimeOnly.Parse(v));


        modelBuilder.Entity<HabitEntity>(builder =>
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Kind).HasConversion<int>();
            builder.Property(h => h.Icon).HasConversion<int>();
            builder.Property(h => h.Color).HasConversion<int>();
            builder.Property(h => h.PartOfTheDay).HasConversion<int?>();
            builder.Property(h => h.State).HasConversion<int>();
            builder.OwnsOne(e => e.Goal);

            builder.Property<DateOnly?>("StartDate").HasConversion(dateOnlyToString);
            builder.Property<DateOnly?>("EndDate").HasConversion(dateOnlyToString);


            // 1-1: Habit -> Reminder (опционально)
            builder.HasOne(h => h.Reminder)
            .WithOne()
            .HasForeignKey<HabitReminderEntity>(r => r.Id)
            .IsRequired(false);


            // 1-1: Habit -> Schedule (обязательно)
            builder.HasOne(h => h.Regularity)
            .WithOne()
            .HasForeignKey<HabitScheduleEntity>(s => s.Id)
            .IsRequired();
        });


        modelBuilder.Entity<HabitReminderEntity>(builder =>
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.StartDate).HasConversion(dateOnlyToString);
            builder.Property(r => r.Time).HasConversion(timeOnlyToString);
        });


        modelBuilder.Entity<HabitScheduleEntity>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property<DateOnly?>("CycleStart").HasConversion(dateOnlyToString); // если есть в домене
            builder.Property(h => h.HabitRegularityType)
                .HasConversion<int>();
        });


        base.OnModelCreating(modelBuilder);
    }
}