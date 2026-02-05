using FlashcardsRevisited.Controllers;
using FlashcardsRevisited.Models;

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
        Console.WriteLine("Create a new stack.");
        Console.WriteLine("------------------");
        Console.WriteLine("Name (mandatory):");
        string nameInput = Console.ReadLine().Trim().ToLower();

        Console.WriteLine("Description (optional):");
        string descriptionInput = Console.ReadLine().Trim().ToLower();

        StackDeck newStack = new()
        {
            StackName = nameInput,
            Description = descriptionInput,
            CreatedDate = DateTime.Now,
        };

        int affectedRows = _stackController.Add(newStack);

        if (affectedRows > 0)
            Console.WriteLine("New Stack created!");
        else
            Console.WriteLine("Couldn't create the stack");
    }

    private void ProcessViewStacks()
    {
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks.Count == 0)
        {
            Console.WriteLine("No stacks found.");
            return;
        }

        foreach (var item in listOfStacks)
        {
            Console.WriteLine($"{item.StackId} - {item.StackName} - {item.Description} - {item.CreatedDate}");
        }
        //TableVisualisation.ShowTable(listOfStacks);
    }
}
