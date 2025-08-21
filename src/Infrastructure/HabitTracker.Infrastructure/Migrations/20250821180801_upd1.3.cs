using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upd13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DaysMachedInCycle",
                table: "HabitSchedules",
                newName: "DaysMatchedInCycle");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Habits",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DaysMatchedInCycle",
                table: "HabitSchedules",
                newName: "DaysMachedInCycle");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Habits",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
