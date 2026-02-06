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

    internal void ProcessViewFlashcards()
    {
        var list = _flashcardController.GetAll();

        if (list.Count == 0)
            Console.WriteLine("No Flashcards found");
        else
            TableVisualisation.ShowTable(list, $"{_currentStack.StackName} Flashcards");
    }

    internal StackDeck? ChooseCurrentStack()
    {
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks.Count == 0)
        {
            Console.WriteLine("No stacks found.");
            return null;
        }
        else
        {
            TableVisualisation.ShowTable(listOfStacks);
        }

        Console.WriteLine("Type the name of the Stack you want:");

        string stackName = Console.ReadLine().Trim().ToLower();

        StackDeck? currentStack = _stackController.GetByName(stackName);

        return currentStack;
    }
}
