# Solution Architecture

## Overview

This file provides information required to quickly and efficiently develop the application. It contains information
on:
- Data design;
- Solution overview.

This file is developed in accordance with the `docs/feature-set.md`.

Schematic is provided; it can be edited with [Excalidraw](https://excalidraw.com).

## Data flow

Data design in this document defines the precise structure of business entities used in the application.

### Data model
The habit structure contains fields:
- **Required** field for selecting a `positive/negative` habit;
- **Required** field for the `name` of the habit;
- **Required** choice of an `icon` for the habit from the catalogue;
- **Required** choice of a `color` for the habit from the catalogue;
- **Required** field for specifying the `goal` with the possibility of selecting a `measurement system` from the `catalogue` (Count, Steps, M, Km, Sec, Min, Hr, Ml, Cal, G, Mg, Drink);
- **Required** field for selecting the `regularity` of repeating the habit:
    1. Daily:
        - Checkbox for daily habit;
        - Choice of days of the week;
        - Choice of number of days per week.

        One of the options **must** be selected.
    1. Monthly:
        - Selection of the days of the month;
        - Selection of the number of days in the month;
    
        One of the options **must** be selected.
    1. Interval:
        - **Required** selection of the frequency of repetition of the habit in days (Every *N* Days);

- **Optional** selection of the `part of the day` (Anytime, Morning, Afternoon, Evening);
- **Optional** selection of the habit `start date`;
- **Optional** selection of the habit `end date`;
- **Optional** `reminder` field (Select Time, Reminder Massage);
- **Optional** `description` of the habit.

- **Internal** state field.

```rust
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
    
    // Internal field
    state: HabitState,
}
```

Every habit can either be *positive*, or *negative*.

```rust
enum GoodnessKind {
    Positive,
    Negative,
}
```

The `icon` of the habit is one of the following:
- Training;
- Running;
- DrinkingWater.

```rust
enum Icon {
    Training,
    Running,
    DrinkingWater,
}
```

Every habit is **required** to have `color`. The dev team decided to use a set of colors, inspired by
ANSI terminal colors.

```rust
enum Color {
    Black,
    Red,
    Green,
    Yellow,
    Blue,
    Magenta,
    Cyan,
    White,
}
```

Feature set required to use the following `measurement unit`s for a habit:
- Count;
- Steps;
- M;
- Km;
- Sec;
- Min;
- Hr;
- Ml;
- Cal;
- G;
- Mg;
- Drink.

```rust
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
```

Every habit **must** have a `goal`.
The goal contains the following fields:
- **Required** field for the name of the goal;
- **Required** field for the `measurement unit` to be used.

```rust
struct Goal {
    name: String,
    unit: MeasurementUnit,
}
```

For setting `regularity` of the habit, the following data types are used:
1. Regularity enumeration that can either be
    - `Daily` of type `DailyRegularity`;
    - `Monthly` of type `MonthlyRegularity`;
    - contain a single integer; the habit is to be executed every *N* days.
1. `DailyRegularity` is either
    - Some specific days of the week (stored as booleans in `WeekDayCollection`), or
    - Some number of times per week (a number).

1. `MonthlyRegularity` is either
    - A set of day numbers, or
    - A single number, representing a general number per month.

```rust
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
```

`Part of the day` is the following enumeration:
- Morning;
- Afternoon;
- Evening.

```rust
enum PartOfTheDay {
    Morning,
    Afternoon,
    Evening,
}
```

The state of the habit is either *incomplete* of *completed*.

```rust
enum State {
    Incomplete,
    Completed,
}
```

Reminder for the habit contains the following fields:
- Time of the reminder;
- Message of the reminder.

```rust
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
    - The `Main` screen appears;
        - The presentation layer calls a method in the application layer to get all habits through the infrastructure;
    - The application layer makes a request to the storage through the infrastructure;
    - The storage retrieves all stored habits and returns as a sorted list;
    - The application layer receives the data from the storage and converts it to the list of `Habit`;
    - The presentation layer receives the list;
        - The presentation layer receives the data and displays the list of habits or a 'No habits' message.
- Description of the data flow for creating a new habit:
    - Pre-requisites: The user is on the `Add` screen.
    - Flow:
    - The presentation layer calls a method in application layer through infrastructure to add new habit;
    - The application layer validates data and makes a request to the storage;
    - The storage retrieves information about successful/unsuccessfull saving of new instance;
    - The application layer receives the information and passes it to application layer;
    - The presentation layer layer navigates user to `Main` screen and displays message about how saving was made.
- Description of the data flow for viewing habit details:
    - Pre-requisites: The `Edit` screen appears.
    - Flow:
    - The presentation layer presents an already loaded habit;
- Description of the data flow for updating a habit:
    - Pre-requisites: The user triggers "Save".
    - Flow: 
    - The presentation layer calls a method in application layer to update habit;
    - The application layer makes a request to the storage;
    - The storage retrieves information about successful/unsuccessfull updating of new instance;
    - The application layer receives the information and passes it to presentation layer;
    - The presentation layer layer navigates user to `Main` screen and displays message about how updating was made.
- Description of the data flow for removing a habit:
    - Pre-requisites: The user triggers "Delete habit".
    - Flow:
    - The presentation layer wait a confirmation to delete a habit;
    - If user is agree, the presentation layer calls method in application layer to delete the habit;
    - The application layer makes a request to the storage;
    - The storage retrieves information about successful/unsuccessfull deleting of new instance;
    - The application layer receives the information and passes it to controller layer;
    - The presentation layer layer navigates user to `habit list` screen and displays message about deleting status.

Notice, that when presentation goes to application layer and application layer to db, all requests
go through infrastructure layer.

## Solution overview

The project is to be developed with onion architecture.
The application is cut into layers, specifically:
- Business entity layer - contains information about business entities;
- Application layer - holds logic and operates on business entities;
- Infrastructure layer - contains two interfaces:
    1. Driving adaptor - for requests; data flows from user to application core;
    1. Driven adaptor - for application working; data flows from application core to external services;

    **Commentary text**

    You can think of driving and driven adaptors as of presentation layer and database interfaces
    respectevly.

    **End commentary text**

- Presentation layer - user interface service; that is what the user interacts with;
- Db application layer - provides a database application for the application's persistence needs;
- Daemon - a background process for doing notifications and marking habits as incomplete.

## User flow

### Navigation

This subsection describes the relationships between presentation layer views (screens) and their dependencies. Each item is a description of the navigation flow for opening 

- Upon opening, the app shows the `Main` screen;
- App can be closed from any screen;
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
    - Result:
        - Navigate to the `Main` screen;
        - A popup with the result is shown.
- Viewing a specific habit\'s details and editing it:
    - Pre-requisites: The user is on the `Main` screen.
    - Action (trigger): The user taps on a specific habit in the list.
    - Result: Navigate to the `Edit` screen to show all stored information for the selected hebit.
- Deleting a habit:
    - Pre-requisites: The user is on the `Edit` screen.
    - Action (trigger): The user presses the `Delete` button and confirms the deletion.
    - Result:
        - After the deletion, navigate to the `Main` screen;
        - The list is updated and no longer shows the deleted habit.
- Canceling the deletion of a habit:
    - Pre-requisites: `Edit` is shown, a confirmation dialog for deletion is shown.
    - Action (trigger): The user presses the button to cancel the deletion.
    - Result:
        - The confirmation dialog is dismissed;
        - The user returns to the `Edit` screen.
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
