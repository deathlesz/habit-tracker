using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Goal_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Goal_Unit = table.Column<int>(type: "INTEGER", nullable: false),
                    Kind = table.Column<int>(type: "INTEGER", nullable: false),
                    Icon = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<int>(type: "INTEGER", nullable: false),
                    PartOfTheDay = table.Column<int>(type: "INTEGER", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<string>(type: "TEXT", nullable: true),
                    EndDate = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabitReminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<string>(type: "TEXT", nullable: false),
                    CyclePatternLength = table.Column<int>(type: "INTEGER", nullable: false),
                    DaysToNotificate = table.Column<string>(type: "TEXT", nullable: false),
                    CyclesToRun = table.Column<int>(type: "INTEGER", nullable: true),
                    Time = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitReminders_Habits_Id",
                        column: x => x.Id,
                        principalTable: "Habits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HabitSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    RepeatingCycleDays = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAnyDay = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAllMachedDays = table.Column<bool>(type: "INTEGER", nullable: false),
                    CycleMachedDaysGoal = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    RepeatingDatesToMatch = table.Column<string>(type: "TEXT", nullable: false),
                    DatesMatched = table.Column<string>(type: "TEXT", nullable: false),
                    DaysMachedInCycle = table.Column<int>(type: "INTEGER", nullable: false),
                    HabitRegularityType = table.Column<int>(type: "INTEGER", nullable: false),
                    CycleStart = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitSchedules_Habits_Id",
                        column: x => x.Id,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitReminders");

            migrationBuilder.DropTable(
                name: "HabitSchedules");

            migrationBuilder.DropTable(
                name: "Habits");
        }
    }
}
