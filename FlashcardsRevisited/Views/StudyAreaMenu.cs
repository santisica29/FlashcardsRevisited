using FlashcardsRevisited.Controllers;
using FlashcardsRevisited.Models;

namespace FlashcardsRevisited.Views;
internal class StudyAreaMenu
{
    StudySessionController _studySessionController;
    StackController _stackController;
    FlashcardController _flashcardController;
    StackDeck? _currentStack;
    public StudyAreaMenu(StackController stackController, FlashcardController flashcardController)
    {
        _stackController = stackController;
        _flashcardController = flashcardController;
        _studySessionController = new();
    }
    internal void MainMenu()
    {
        _currentStack = ChooseCurrentStack();

        bool closeApp = false;
        while (!closeApp)
        {
            Console.WriteLine("Study Area");
            Console.WriteLine("0 - Go back");
            Console.WriteLine("1 - Start study session");
            Console.WriteLine("2 - View study sessions with this stack");
            string userChoice = Console.ReadLine().Trim().ToLower();

            switch (userChoice)
            {
                case "0":
                    closeApp = true;
                    break;
                case "1":
                    ProcessStartStudySession();
                    break;
                case "2":
                    ProcessViewStudySessions();
                    break;
            }
        }  
    }

    internal void ProcessStartStudySession()
    {
        int score = 0;
        var flashcardList = _flashcardController.GetFlashcardsFromStack(_currentStack.StackId);

        if (flashcardList.Count == 0)
        {
            Console.WriteLine("No flashcards found in this stack");
            return;
        }

        foreach (var flashcard in flashcardList)
        {
            Console.WriteLine($"{flashcard.Front}:");
            string answer = Console.ReadLine().Trim().ToLower();

            if (answer == flashcard.Back)
            {
                score++;
                Console.WriteLine("Correct answer!");
            }
            else
            {
                Console.WriteLine("Incorrect answer.");
            }

            Console.ReadKey();
        }

        Console.WriteLine($"Final score {score}/{flashcardList.Count} pts");
        Console.ReadKey();

        StudySession newSession = new()
        {
            StackId = _currentStack.StackId,
            Score = score,
            DateOfSession = DateTime.Now,
        };

        var affectedRows = _studySessionController.Add(newSession);

        if (affectedRows > 0)
            Console.WriteLine("Study session added correctly!");
        else
            Console.WriteLine("Couldn't saved study session");
    }

    internal void ProcessViewStudySessions()
    {
        throw new NotImplementedException();
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
            TableVisualisation.ShowStacks(listOfStacks);
        }

        Console.WriteLine("Type the name of the Stack you want:");

        string stackName = Console.ReadLine().Trim().ToLower();

        StackDeck? currentStack = _stackController.GetByName(stackName);

        return currentStack;
    }
}
