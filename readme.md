# Flashcard Project
Console app to study by building flashcards.

Develop using C#, SQL Server to store data (with Dapper as ORM) and Spectre.Console for the user interface.

## Requirements

- This is an application where the users will create Stacks of Flashcards.

- You'll need two different tables for stacks and flashcards. The tables should be linked by a foreign key.

- Stacks should have an unique name.

- Every flashcard needs to be part of a stack. If a stack is deleted, the same should happen with the flashcard.

- You should use DTOs to show the flashcards to the user without the Id of the stack it belongs to.

- When showing a stack to the user, the flashcard Ids should always start with 1 without gaps between them. If you have 10 cards and number 5 is deleted, the table should show Ids from 1 to 9.

- After creating the flashcards functionalities, create a "Study Session" area, where the users will study the stacks. Study sessions should be stored, with date and score.

- The study and stack tables should be linked. If a stack is deleted, it's study sessions should be deleted.

- The project should contain a call to the study table so the users can see all their study sessions. This table receives insert calls upon each study session, but there shouldn't be update and delete calls to it.

## Features

- SQL Server database connection
	- The program uses a SQL Server db connection to store and read information.
    - If no database exists, or the correct table does not exist they will be created on program start.
    - Using Dapper as a ORM to connect and query the database in a cleaner fashion.

- Spectre.Console for the UI, to improve readability.

- Muliple menus for different areas (Stacks, Flashcards, Study Area).

- CRUD DB functions
    - Users can create, read, update or delete Stacks and Flashcards.
    - Users can read and delete Study Sessions.
    - Users can see the exact number of flashcards in each stack.

- Reporting and other data output uses ConsoleTableExt library to output in a more pleasant way.

## Challenges

- Work with linked tables and reference them correctly.
- Using DTOs correctly to present the user the information in a safer way.
- Going from SQLite to SQL Server and the differences in between them.
- Create multiple menus for different areas while keeping everything organize.
- Using join to access data from another table.
- Validating the inputs so the app doesn't crash

## Resources

- [Project idea from The C# Sharp Academy](https://www.thecsharpacademy.com/project/14/flashcards)
- Dapper documentation
- SQL Server documentation
