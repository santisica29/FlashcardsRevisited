using Spectre.Console;

namespace FlashcardsRevisited.Helpers;
internal static class UserInterface
{
    internal static void DisplayMessage(string message, string color = "blue")
    {
        AnsiConsole.MarkupLine($"[{color}]{message}[/]");
    }
}
