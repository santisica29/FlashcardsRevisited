using FlashcardsRevisited.Controllers;
using FlashcardsRevisited.Models;
using System.Threading.Channels;

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
        bool closeApp = false;

        while (!closeApp)
        {
            Console.WriteLine("Study Area");
            Console.WriteLine("0 - Go back");
            Console.WriteLine("1 - Start study session");
            Console.WriteLine("2 - View study sessions in a stack");
            Console.WriteLine("3 - View all study sessions");
            Console.WriteLine("4 - Change current stack");

            string userChoice = Console.ReadLine().Trim().ToLower();

            switch (userChoice)
            {
                case "0":
                    closeApp = true;
                    break;
                case "1":
                    _currentStack ??= ChooseCurrentStack();
                    ProcessStartStudySession();
                    break;
                case "2":
                    //if (left) is null, assign (right)
                    _currentStack ??= ChooseCurrentStack();
                    ProcessViewStudySessionsInStack();
                    break;
                case "3":
                    ProcessViewAllStudySessions();
                    break;
                case "4":
                    Console.WriteLine($"Current stack: {_currentStack.StackName}");
                    _currentStack = ChooseCurrentStack();
                    break;
            }
        }
    }

    private void ProcessViewAllStudySessions()
    {
        List<StudySessionDTO> list = _studySessionController.GetAll();

        if (list == null)
            Console.WriteLine("No study sessions.");
        else
            TableVisualisation.ShowStudySessions(list, "Study Sessions");
    }

    internal void ProcessStartStudySession()
    {
        var flashcardList = _flashcardController.GetFlashcardsFromStack(_currentStack.StackId);

        if (flashcardList.Count == 0)
        {
            Console.WriteLine("No flashcards found in this stack");
            return;
        }

        int score = 0;

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
            Score = score,
            DateOfSession = DateTime.Now,
            Stack = _currentStack,
        };

        var affectedRows = _studySessionController.Add(newSession);

        if (affectedRows > 0)
            Console.WriteLine("Study session added correctly!");
        else
            Console.WriteLine("Couldn't saved study session");
    }

    internal void ProcessViewStudySessionsInStack()
    {
        var sessions = _studySessionController.GetSessionsFromStack(_currentStack.StackId);

        if (sessions.Count == 0)
        {
            Console.WriteLine("No sessions found in this stack");
            return;
        }

        TableVisualisation.ShowFlashcards(sessions, $"{_currentStack.StackName} sessions");
    }

    internal StackDeck? ChooseCurrentStack()
    {
        var listOfStacks = _stackController.GetAll();

        if (listOfStacks.Count == 0)
        {
            Console.WriteLine("No stacks found.");
            return null;
        }

        TableVisualisation.ShowStacks(listOfStacks);

        StackDeck? currStack = null;

        while (currStack == null)
        {
            Console.WriteLine("Type the name of the Stack you want:");

            string stackName = Console.ReadLine().Trim().ToLower();

            currStack = _stackController.GetByName(stackName);

            if (currStack == null)
                Console.WriteLine("Stack not found. Try again.");
        }

        return currStack;
    }
}
