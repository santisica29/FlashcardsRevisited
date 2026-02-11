using FlashcardsRevisited.Controllers;
using FlashcardsRevisited.Models;
using static FlashcardsRevisited.Helpers.UserInterface;

namespace FlashcardsRevisited.Views;
internal class FlashcardsMenu
{
    private readonly FlashcardController _flashcardController;
    private readonly StackController _stackController;
    private StackDeck? _currentStack;

    public FlashcardsMenu(StackController stackController)
    {
        _stackController = stackController;
        _flashcardController = new();
    }
    internal void MainMenu()
    {
        Console.Clear();

        _currentStack = ChooseCurrentStack();

        while (_currentStack == null)
        {
            Console.WriteLine("Invalid input");
            Console.ReadKey();

            MainMenu();
            return;
        }

        bool closeFlashcardMenu = false;

        while (!closeFlashcardMenu)
        {
            Console.Clear();

            Console.WriteLine($"Current Stack: {_currentStack.StackName}\n");
            Console.WriteLine("Flashcards Menu.");
            Console.WriteLine("1 - View all Flashcards in stack");
            Console.WriteLine("2 - Create a Flashcard in current stack");
            Console.WriteLine("3 - Edit");
            Console.WriteLine("4 - Delete");
            Console.WriteLine("0 - Go Back");

            string userInput = Console.ReadLine().Trim().ToLower();

            switch (userInput)
            {
                case "1":
                    ProcessViewFlashcards();
                    break;
                case "2":
                    ProcessCreateFlashcard();
                    break;
                case "3":
                    ProcessUpdateFlashcard();
                    break;
                case "4":
                    ProcessDeleteFlashcard();
                    break;
                case "0":
                    return;
            }
            
            Console.ReadKey();
        }
    }

    private void ProcessDeleteFlashcard()
    {
        var list = ProcessViewFlashcards();

        Console.WriteLine("Select the Id of the flashcard you want to delete.");
        string id = Console.ReadLine().Trim().ToLower();

        int idAsInt;

        while (!int.TryParse(id, out idAsInt))
        {
            DisplayMessage("Invalid input. You must enter a number. Try again", "red");

            Console.WriteLine("Select the Id of the flashcard you want to delete.");
            id = Console.ReadLine().Trim().ToLower();
        }
        
        int idToDelete = 0;

        foreach (var item in list)
        {
            if (item.DTOId == idAsInt)
                idToDelete = item.FlashcardId;
        }

        var affectedRows = _flashcardController.Delete(idToDelete);

        if (affectedRows > 0)
            DisplayMessage($"Flashcard with id {idToDelete} was deleted.", "green");
        else
            DisplayMessage("Couldn't delete flashcard", "red");
    }

    private void ProcessUpdateFlashcard()
    {
        var list = ProcessViewFlashcards();

        DisplayMessage("Select the Id of the flashcard you want to update.");
        string id = Console.ReadLine().Trim().ToLower();

        int idAsInt;

        while (!int.TryParse(id, out idAsInt))
        {
            DisplayMessage("Invalid input. You must enter a number. Try again", "red");

            Console.WriteLine("Select the Id of the flashcard you want to delete.");
            id = Console.ReadLine().Trim().ToLower();
        }

        int idToUpdate = 0;

        foreach (var item in list)
        {
            if (item.DTOId == idAsInt)
                idToUpdate = item.FlashcardId;
        }

        var flashcardToUpdate = _flashcardController.GetById(idToUpdate);

        if (flashcardToUpdate == null)
        {
            DisplayMessage("No flashcard found with that id", "red");
            Console.ReadKey();

            Console.Clear();
            ProcessUpdateFlashcard();
            return;
        }

        bool updating = true;

        while (updating)
        {
            Console.WriteLine("Choose which area you want to update:");
            Console.WriteLine($"Current front: {flashcardToUpdate.Front} - Current back: {flashcardToUpdate.Back}");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("1 - Front");
            Console.WriteLine("2 - Back");
            Console.WriteLine("3 - Stop updating");
            Console.WriteLine("0 - Go back");

            string userInput = Console.ReadLine().Trim().ToLower();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Enter new front:");
                    string newFront = Console.ReadLine();

                    flashcardToUpdate.Front = newFront;
                    break;
                case "2":
                    Console.WriteLine("Enter new back:");
                    string newBack = Console.ReadLine();

                    flashcardToUpdate.Back = newBack;
                    break;
                case "3":
                    updating = false;
                    break;
                case "0":
                    updating = false;
                    MainMenu();
                    return;
            }
        }

        var affectedRows = _flashcardController.Update(flashcardToUpdate);

        if (affectedRows > 0)
            DisplayMessage($"Flashcard with id {idToUpdate} was updated.", "green");
        else
            DisplayMessage("Couldn't update flashcard", "red");
    }

    internal void ProcessCreateFlashcard()
    {
        Console.WriteLine("Choose the front:");
        string front = Console.ReadLine().Trim().ToLower();

        Console.WriteLine("Choose the back:");
        string back = Console.ReadLine().Trim().ToLower();

        Flashcard newFlashcard = new()
        {
            Front = front,
            Back = back,
            Stack = _currentStack
        };

        var rowsAffected = _flashcardController.Add(newFlashcard);

        if (rowsAffected > 0)
            DisplayMessage("Flashcard created!", "green");
        else
            DisplayMessage("Couldn't create the flashcard.", "red");
    }

    internal List<FlashcardDTO> ProcessViewFlashcards()
    {
        var list = _flashcardController.GetFlashcardsFromStack(_currentStack.StackId);

        if (list.Count == 0)
            DisplayMessage($"No Flashcards found in {_currentStack.StackName} stack.", "red");
        else
            TableVisualisation.ShowFlashcards(list, $"{_currentStack.StackName} Flashcards");

        return list;
    }

    internal StackDeck? ChooseCurrentStack()
    {
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks.Count == 0)
        {
            DisplayMessage("No stacks found.", "red");
            return null;
        }
        else
        {
            TableVisualisation.ShowStacks(listOfStacks);
        }

        Console.WriteLine("Type the name of the Stack you want for your flashcards:");

        string stackName = Console.ReadLine().Trim().ToLower();

        StackDeck? currentStack = _stackController.GetByName(stackName);

        return currentStack;
    }
}
