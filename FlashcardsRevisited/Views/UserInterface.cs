using FlashcardsRevisited.Controllers;
using static FlashcardsRevisited.Views.TableVisualisation;

namespace FlashcardsRevisited.Views;
internal class UserInterface
{
    StackController stackController = new();
    FlashcardController flashcardController = new();

    internal void MainMenu()
    {
        bool closeApp = false;

        while (!closeApp)
        {
            Console.WriteLine("Main Menu.");
            Console.WriteLine("-----------");
            Console.WriteLine("Manage Stacks (press s)");
            Console.WriteLine("Manage Flashcards (press f)");
            Console.WriteLine("Study Area (press x)");
            Console.WriteLine("Exit (press 0)");
            Console.WriteLine("-----------");
            Console.WriteLine("Choose one.");

            var commandInput = Console.ReadLine().Trim().ToLower();

            switch (commandInput)
            {
                case "s":
                    StacksMenu();
                    break;
                case "f":
                    FlashcardsMenu();
                    break;
                case "x":
                    StudyAreaMenu();
                    break;
                case "0":
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }

    private void StudyAreaMenu()
    {
        throw new NotImplementedException();
    }

    private void FlashcardsMenu()
    {
        throw new NotImplementedException();
    }

    private void StacksMenu()
    {
        bool closeStackMenu = false;

        while (!closeStackMenu)
        {
            Console.WriteLine("Stacks Menu.");
            Console.WriteLine("View All (v)");
            Console.WriteLine("Create (c)");
            Console.WriteLine("Update (u)");
            Console.WriteLine("Delete (d)");
            Console.WriteLine("Go Back (0)");

            string userInput = Console.ReadLine().Trim().ToLower();

            switch (userInput)
            {
                case "v":
                    ProcessViewStacks();
                    break;
                case "c":
                    ProcessCreateStack();
                    break;
                case "u":
                    ProcessUpdateStack();
                    break;
                case "d":
                    ProcessDeleteStack();
                    break;
                case "0":
                    return;
            }
        }
        
    }

    private void ProcessDeleteStack()
    {
        throw new NotImplementedException();
    }

    private void ProcessUpdateStack()
    {
        throw new NotImplementedException();
    }

    private void ProcessCreateStack()
    {
        throw new NotImplementedException();
    }

    private void ProcessViewStacks()
    {
        var listOfStacks = stackController.GetAll();

        if (listOfStacks.Count == 0)
            Console.WriteLine("No stacks found.");
            return;

        TableVisualisation.ShowTable(listOfStacks);    
    }
}
