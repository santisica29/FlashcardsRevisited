using FlashcardsRevisited.Controllers;
using FlashcardsRevisited.Models;

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
        _currentStack = ChooseCurrentStack();

        if (_currentStack == null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        bool closeFlashcardMenu = false;

        while (!closeFlashcardMenu)
        {
            Console.WriteLine($"Current Stack: {_currentStack.StackName}");
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
        }
    }

    private void ProcessDeleteFlashcard()
    {
        throw new NotImplementedException();
    }

    private void ProcessUpdateFlashcard()
    {
        throw new NotImplementedException();
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
            Console.WriteLine("Flashcard created!");
        else
            Console.WriteLine("Couldn't create the flashcard.");
    }

    internal void ProcessViewFlashcards()
    {
        var list = _flashcardController.GetFlashcardsFromStack(_currentStack.StackId);

        if (list == null)
            Console.WriteLine($"No Flashcards found in {_currentStack.StackName} stack.");
        else
            TableVisualisation.ShowFlashcards(list, $"{_currentStack.StackName} Flashcards");
    }

    internal StackDeck? ChooseCurrentStack()
    {
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks == null)
        {
            Console.WriteLine("No stacks found.");
            return null;
        }
        else
        {
            TableVisualisation.ShowStacks(listOfStacks);
        }

        Console.WriteLine("Type the name of the Stack you want:");

        string stackName = Console.ReadLine().Trim().ToLower();

        StackDeck? currentStack = _stackController.GetByName(stackName);

        return currentStack;
    }
}
