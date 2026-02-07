using FlashcardsRevisited.Controllers;
using FlashcardsRevisited.Models;

namespace FlashcardsRevisited.Views;

internal class StacksMenu
{
    private StackController _stackController;

    public StacksMenu(StackController stackController)
    {
        _stackController = stackController;
    }

    internal void MainMenu()
    {
        bool closeStackMenu = false;

        while (!closeStackMenu)
        {
            Console.WriteLine("Stacks Menu.");
            Console.WriteLine("1 - View All");
            Console.WriteLine("2 - Create");
            Console.WriteLine("3 - Update");
            Console.WriteLine("4 - Delete");
            Console.WriteLine("0 - Go Back");

            string userInput = Console.ReadLine().Trim().ToLower();

            switch (userInput)
            {
                case "1":
                    ProcessViewStacks();
                    break;
                case "2":
                    ProcessCreateStack();
                    break;
                case "3":
                    ProcessUpdateStack();
                    break;
                case "4":
                    ProcessDeleteStack();
                    break;
                case "0":
                    return;
            }
        }
    }

    internal void ProcessDeleteStack()
    {
        ProcessViewStacks();

        Console.WriteLine("Type the name of the Stack you want to delete");
        string nameSelected = Console.ReadLine().Trim().ToLower();

        StackDeck? stackToDelete = _stackController.GetByName(nameSelected);

        if (stackToDelete == null)
        {
            Console.WriteLine($"No Stack found called {nameSelected}.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Are you sure you want to delete {stackToDelete.StackName}? (y/n)");
        string confirmationInput = Console.ReadLine().Trim().ToLower();

        if (confirmationInput != "y")
        {
            Console.WriteLine("Deletion canceled.");
            Console.ReadKey();
            return;
        }
        
        int rowsAffected = _stackController.Delete(stackToDelete.StackId);

        if (rowsAffected > 0)
            Console.WriteLine("Stack deleted");
        else
            Console.WriteLine("Couldn't delete the Stack");

        Console.ReadKey();
    }

    private void ProcessUpdateStack()
    {
        ProcessViewStacks();

        Console.WriteLine("Type the name of the Stack you want to update");
        string nameSelected = Console.ReadLine().Trim().ToLower();

        StackDeck? stackToUpdate = _stackController.GetByName(nameSelected);

        if (stackToUpdate == null)
        {
            Console.WriteLine("No Stack found with that name. Try Again");
            Console.ReadKey();
            return;
        }

        bool updating = true;

        while (updating)
        {
            Console.WriteLine("Select which part you want to update.");
            Console.WriteLine("Press 1 to update the name");
            Console.WriteLine("Press 2 to update the description");
            Console.WriteLine("Press s to stop updating");
            Console.WriteLine("Press 0 to go back");

            var cmd = Console.ReadLine().Trim().ToLower();

            switch (cmd)
            {
                case "1":
                    Console.WriteLine("Type the new name:");
                    string newName = Console.ReadLine();
                    stackToUpdate.StackName = newName;
                    break;
                case "2":
                    Console.WriteLine("Type the new name:");
                    string newDescription = Console.ReadLine();
                    stackToUpdate.Description = newDescription;
                    break;
                case "s":
                    updating = false;
                    break;
                case "0":
                    updating = false;
                    MainMenu();
                    return;
            }
        }

        int rowsAffected = _stackController.Update(stackToUpdate);

        if (rowsAffected > 0)
            Console.WriteLine($"Stack with the name of {stackToUpdate.StackName} was updated.");
        else
            Console.WriteLine("Couldn't update the stack.");
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

        int rowsAffected = _stackController.Add(newStack);

        if (rowsAffected > 0)
            Console.WriteLine("New Stack created!");
        else
            Console.WriteLine("Couldn't create the stack");
    }

    private void ProcessViewStacks()
    {
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks.Count == 0)
            Console.WriteLine("No stacks found.");
        else
            TableVisualisation.ShowStacks(listOfStacks, "Stacks");
    }
}
