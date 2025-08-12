# Solution Architecture

## Overview

This file provides information required to quickly and efficiently develop the application. It contains information
on:
- Data design;
- Solution overview.

This file is developed in accordance with the `docs/feature-set.md`.

## Data flow

Data design in this document defines the precise structure of business entities used in the application.

### Data model

In the application the following entities are defined and used:

```rust
enum GoodnessKind {
    Positive,
    Negative,
}

enum Icon {
    Training,
    Running,
    DrinkingWater
}

// The dev team decided to use ANSI terminal colors
enum Color {
    Black,
    Red,
    Green,
    Yellow,
    Blue,
    Magenta,
    Cyan,
    White
}

enum MeasurementUnit {
    Count,
    Steps,
    M,
    Km,
    Sec,
    Min,
    Hr,
    Ml,
    Cal,
    G,
    Mg,
    Drink
}

struct Goal {
    name: String,
    unit: MeasurementUnit,
}

struct WeekDayCollection {
    Monday: bool,
    Tuesday: bool,
    Wednesday: bool,
    Thursday: bool,
    Friday: bool,
    Saturday: bool,
    Sunday: bool
}

enum Regularity {
    Daily(DailyRegularity),
    Monthly(MonthlyRegularity),
    EveryNDays(i32),
}
enum DailyRegularity {
    DayOfTheWeek(WeekDayCollection),
    TimesPerWeek(i32),
}

enum MonthlyRegularity {
    // if nth bit is set, nth day is counted.
    ConcreteDays(i32),

    TimesPerMonth(i32),
}

enum PartOfTheDay {
    Morning,
    Afternoon,
    Evening,
}

struct Habit {
    // Required fields
    kind: GooodnessKind,
    name: String,
    icon: Icon,
    color: Color,
    goal: Goal,

    // Optional fields
    part_of_the_day: Option<PartOfTheDay>,
    start_date: Option<DateOnly>,
    end_date: Option<DateOnly>,
    reminder: Option<Remainder>,
    description: Option<String>,

    state: State,
}

enum State {
    Incomplete,
    Completed,
}

struct Reminder {
    time: TimeOnly,
    message: String,
}
```
### Data flow

#### Data Flow Descriptions

- Display all habits:
    - Pre-requisites: The user is on the `Main` screen.
    - Flow:
	- The `Main` screen appears.
        - The presentation layer calls a method in the logic layer to get all habits through the infrastructure.
	- The logic layer makes a request to the storage through the infrastructure.
	- The storage retrieves all stored habits and returns as a sorted list.
	- The logic layer receives the data from the storage and converts it to the list of `Habit`.
	- The presentation layer receives the list.
        - The presentation layer receives the data and displays the list of habits or a 'No habits' message.
- Description of the data flow for creating a new habit:
    - Pre-requisites: The user is on the `Add` screen.
    - Flow:
	- The presentation layer calls a method in logic layer through infrastructure to add new habit.
	- The logic layer validates data and makes a request to the storage.
	- The storage retrieves information about successful/unsuccessfull saving of new instance.
	- The logic layer receives the information and passes it to logic layer.
	- The presentation layer layer navigates user to `Main` screen and displays message about how saving was made.
- Description of the data flow for viewing habit details:
    - Pre-requisites: The `Edit` screen appears.
    - Flow:
	- The presentation layer presents an already loaded habit.
- Description of the data flow for updating a habit:
    - Pre-requisites: The user triggers "Save".
    - Flow: 
	- The presentation layer calls a method in logic layer to update habit.
	- The logic layer makes a request to the storage.
	- The storage retrieves information about successful/unsuccessfull updating of new instance.
	- The logic layer receives the information and passes it to presentation layer.
	- The presentation layer layer navigates user to `Main` screen and displays message about how updating was made.
- Description of the data flow for removing a habit:
    - Pre-requisites: The user triggers "Delete habit".
    - Flow:
	- The presentation layer wait a confirmation to delete a habit.
	- If user is agree, the presentation layer calls method in logic layer to delete the habit.
	- The logic layer makes a request to the storage.
	- The storage retrieves information about successful/unsuccessfull deleting of new instance.
	- The logic layer receives the information and passes it to controller layer.
	- The presentation layer layer navigates user to `habit list` screen and displays message about deleting status.

Notice, that when presentation goes to logic layer and logic layer to db, all requests
go through infrastructure layer.

## Solution overview

The project is to be developed with onion architecture.
The application is cut into layers, specifically:
- Business entity layer - contains information about business entities;
- Logic layer - holds logic and operates on business entities;
- Infrastructure layer - contains two interfaces:
    1. Driving adaptor - for requests; data flows from user to application core;
    1. Driven adaptor - for application working; data flows from application core to external services;

    **Commentary text**

    You can think of driving and driven adaptors as of presentation layer and database interfaces
    respectevly.

    **End commentary text**

- Presentation layer - user interface service; that is what the user interacts with;
- Db logic layer - provides a database logic for the application's persistence needs;
- Daemon - a background process for doing notifications and marking habits as incomplete.

## User flow

### Navigation

This subsection describes the relationships between presentation layer views (screens) and their dependencies. Each item is a description of the navigation flow for opening 

- Upon opening, the app shows the `Main` screen.
- App can be closed from any screen.
- Opening the `Add` screen:
    - Pre-requisites: The user is on the `Main` screen.
    - Action (trigger): The user presses the `Add` button.
    - Result: Navigate to the `Add` screen with empty input fields.
- Canceling adding habit:
    - Pre-requisites: The user is on the `Add` screen.
    - Action (trigger): The user presses the `Cancel` button.
    - Result: Navigate to the `Main` screen.
- Saving a new habit:
    - Pre-requisites: The user is on the `Add` screen.
    - Action (trigger): The user presses the `Save` button.
    - Result: Navigate to the `Main` screen. A popup with the result is shown.
- Viewing a specific habit\'s details and editing it:
    - Pre-requisites: The user is on the `Main` screen.
    - Action (trigger): The user taps on a specific habit in the list.
    - Result: Navigate to the `Edit` screen to show all stored information for the selected hebit.
- Deleting a habit:
    - Pre-requisites: The user is on the `Edit` screen.
    - Action (trigger): The user presses the `Delete` button and confirms the deletion.
    - Result: After the deletion, navigate to the `Main` screen. The list is updated and no longer shows the deleted habit.
- Canceling the deletion of a habit:
    - Pre-requisites: `Edit` is shown, a confirmation dialog for deletion is shown.
    - Action (trigger): The user presses the button to cancel the deletion.
    - Result: The confirmation dialog is dismissed, and the user see the `Edit` screen.
- Updating and saving the habit:
    - Pre-requisites: The user is on the `Edit` screen and has changed some information.
    - Action (trigger): The user presses the `Save` button.
    - Result: After the data is saved, navigate to the `Main` screen to show the updated information.
- Canceling the update of the habit:
    - Pre-requisites: The user is on the `Edit` screen.
    - Action (trigger): The user presses the `Cancel` button and confirms the action.
    - Result: Navigate back to the `Main` screen without saving any changes.
- Viewing global statistics:
    - Pre-requisites: The user is on `Main` screen.
    - Action (trigger): The user presses the `Statistics` button.
    - Result: Navigate to `Statistics` screen.
- Exiting the `Statistics` screen:
    - Pre-requisites: The user is on `Statistis` screen.
    - Action (trigger): The user presses the `Exit` button.
    - Result: Navigate back to `Main` screen.