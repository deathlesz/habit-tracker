# Feature set document

This document aims to provide concise description of the application functionality from the stand point of an abstract user.
It contains a list of all supported features and their descriptions, as well as a general product description.

## Product description

*Habit Tracker* is a simple tool that attempts to make your life better.

It makes tracking both good and bad habits easy, visualizes your progress in a clear and concise way and helps you to stay accountable.

## Adding habits

### General form design

After clicking the `Add Habit` button, the following fields are displayed:  

**Required** fields:
- `Habit type` - selection between `Positive` or `Negative` habit;
- `Habit name` - field for entering the habit’s name;
- `Icon selection` - choice of an icon from the icon catalogue;
- `Color selection` - choice of a color from the color catalogue;
- `Goal setting` - field for specifying the habit’s goal, with the ability to select a measurement unit from the catalogue (Count, Steps, M, Km, Sec, Min, Hr, Ml, Cal, G, Mg, Drink);
- `Regularity selection` - field for setting the habit’s repetition frequency:  
    1. Daily: 
        - Checkbox for daily habit;
        - Choice of specific days of the week;
        - Option to select the number of days per week.
        
        At least one of these options **must** be selected. 
    1. Monthly:  
        - Selection of specific days in the month;
        - Option to set the number of days per month.
        
        At least one of these options **must** be selected. 
    1. Interval:
        - **Required** selection of habit repetition frequency in days (Every *N* Days).

**Optional** fields:
- `Time of day` - selection of when the habit is performed (Anytime, Morning, Afternoon, Evening);
- `Start date` - selection of the habit’s start date;
- `End date` - selection of the habit’s end date;
- `Reminder` - reminder settings (Select Time, Reminder Message);
- `Description` - field for adding additional notes about the habit.

### Validation & Saving

- When the `Save` button is clicked, **all** fields are validated;
- If any **required** field is missing or incorrect, the user sees the problematic field along with an error message;  
- **If** validation passes, the habit is saved to the database;
- Upon successful saving, the user sees a confirmation message and is redirected to the habit list.

### Canceling

Clicking the `Cancel` button resets the form and returns the user to the habit list without saving.

## Editing habits

When the `Edit Habit` button is clicked, the habit form opens with all existing fields populated with the current habit data.  

### Validation & Editing

- If **any** field contains incorrect data, it is highlighted with an *error message explaining the issue*;
- The user can modify **any** field, including:
  - `Habit Type` (Positive/Negative);
  - `Habit Name`;
  - `Icon` & `Color` selection;
  - `Goal` & `Measurement Unit`;
  - `Regularity Settings` (Daily, Monthly, Interval);
  - Any **optional** field (`Time of day`, `Start/End date`, `Reminder`, `Description`).

### Saving Changes

- When the `Save` button is clicked, **all** fields are validated;
- **If** validation passes, the updated habit is saved to the database;
- The user sees a success message confirming the changes and is redirected to the habit list.

### Canceling Changes

If the user clicks `Cancel`, a confirmation dialog appears with the message:  
  ```
  All unsaved changes will be lost. Are you sure you want to exit?
  ```
  - On `Agree` action, the form resets, and the user returns to the habit list without saving;
  - On `Decline` action, the dialog closes, and the user continues editing with all modifications preserved.

## Actions for habits

Actions for `Incomplete` Habits:
- `Mark as Complete` - Swipe to fill the progress bar and confirm completion;
- `Edit Habit` - Modify habit settings;
- `Delete Habit` - Remove the habit entirely from your list;
- `View Statistics` - Open statistics for this specific habit.

Actions for `Completed` Habits:
- `Edit Habit` - Modify habit settings;
- `Reset Habit` - Clear completion status;
- `Delete Habit` - Permanently remove the habit from the list;
- `View Statistics` - Open statistics for this specific habit.

If the user does not mark a habit as `Completed` before the end of its execution period, the habit is **automatically** marked as `Incomplete`.

## Statistics

### Statistics for a specific habit

#### Calendar

Days on the calendar have three types of highlighting:

1. `Neutral` (no habit was scheduled for that day);
1. `Partially completed` (the habit was not completely fulfilled);
1. `All completed` (all scheduled habits for the day were accomplished).

Also the following information is provided:

- Information on the *current series* of habit execution;
- Information on the *record series* of habit execution;
- Information on the *total amount* of habit execution;
- Information about *not completing* the habit.

### Statistics for all habits

#### Calendar

Days on the calendar have three types of highlighting:

1. `Neutral` (no habit was scheduled for that day);
1. `Partially` completed (not all scheduled habits were completed);
1. `All completed` (all scheduled habits for the day were completed).

- Information on the *current series* of all habits execution;
- Information on the *record series* of all habits execution;
- Information on the *total amount* of habits execution;
- Information about *not completed* of all habits.
