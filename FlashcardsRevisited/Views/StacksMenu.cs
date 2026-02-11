using FlashcardsRevisited.Controllers;
using FlashcardsRevisited.Models;
using Spectre.Console;
using static FlashcardsRevisited.Helpers.UserInterface;

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
            Console.Clear();

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

            Console.ReadKey();
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
            DisplayMessage($"No Stack found called {nameSelected}.", "red");
            Console.ReadKey();
            return;
        }

        bool confirmationInput = AnsiConsole.Confirm($"Are you sure you want to delete {stackToDelete.StackName}?");

        if (!confirmationInput)
        {
            DisplayMessage("Deletion canceled.");
            Console.ReadKey();
            return;
        }

        int rowsAffected = _stackController.Delete(stackToDelete.StackId);

        if (rowsAffected > 0)
            DisplayMessage("Stack deleted", "green");
        else
            DisplayMessage("Couldn't delete the Stack", "red");
    }

    private void ProcessUpdateStack()
    {
        ProcessViewStacks();

        DisplayMessage("Type the name of the Stack you want to update");
        string nameSelected = Console.ReadLine().Trim().ToLower();

        StackDeck? stackToUpdate = _stackController.GetByName(nameSelected);

        if (stackToUpdate == null)
        {
            DisplayMessage("No Stack found with that name. Try Again", "red");
            Console.ReadKey();

            Console.Clear();
            ProcessUpdateStack();
            return;
        }

        bool updating = true;

        while (updating)
        {
            Console.Clear();

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
                    Console.WriteLine("Type the new description:");
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
            Console.ReadKey();
        }

        int rowsAffected = _stackController.Update(stackToUpdate);

        if (rowsAffected > 0)
            DisplayMessage($"Stack with the name of {stackToUpdate.StackName} was updated.", "green");
        else
            DisplayMessage("Couldn't update the stack.", "red");
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
            DisplayMessage("New Stack created!", "green");
        else
            DisplayMessage("Couldn't create the stack", "red");
    }

    private void ProcessViewStacks()
    {
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks.Count == 0)
            DisplayMessage("No stacks found.", "red");
        else
            TableVisualisation.ShowStacks(listOfStacks, "Stacks");
    }
}
