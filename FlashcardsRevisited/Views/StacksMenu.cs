using FlashcardsRevisited.Controllers;

namespace FlashcardsRevisited.Views;

internal class StacksMenu
{
    private StackController _stackController = new();

    internal void MainMenu()
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

    internal void ProcessDeleteStack()
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
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks.Count == 0)
        {
            Console.WriteLine("No stacks found.");
            return;
        }

        TableVisualisation.ShowTable(listOfStacks);
    }
}
