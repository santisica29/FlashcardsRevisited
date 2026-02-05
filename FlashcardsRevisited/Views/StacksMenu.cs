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
        ProcessViewStacks();

        Console.WriteLine("Type the ID of the Stack you want to delete");
        string idSelected = Console.ReadLine().Trim().ToLower();

        int idAsInt = Convert.ToInt32(idSelected);

        StackDeck? stackToDelete = _stackController.GetById(idAsInt);

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
            Console.WriteLine("No stacks found.");
        else
            TableVisualisation.ShowTable(listOfStacks, "Stacks");
    }
}
