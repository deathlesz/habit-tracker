using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace HabitTracker.Infrastructure.DesignTime;
using HabitTracker.Infrastructure;


public class DesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
	public AppDbContext CreateDbContext(string[] args)
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseSqlite("Data Source=habittracker_migrations.db", b =>
			{
				b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
			})
			.Options;
		return new AppDbContext(options);
	}
}
