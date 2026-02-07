using ConsoleTableExt;
using FlashcardsRevisited.Models;

namespace FlashcardsRevisited.Views;

internal class TableVisualisation
{
    internal static void ShowStacks(List<StackDeck> tableData, string title = "Stacks")
    {
        Console.WriteLine("\n\n");

        ConsoleTableBuilder
            .From(tableData)
            .WithTitle(title, ConsoleColor.DarkGreen)
            .ExportAndWriteLine();

        Console.WriteLine("\n\n");
    }

    internal static void ShowFlashcards(List<FlashcardDTO> tableData, string title)
    {
        Console.WriteLine("\n\n");
        int id = 1;
        var rows = tableData.Select(f => new List<object>
        {
            id++,
            f.Front,
            f.Back,
        }).ToList();

        ConsoleTableBuilder
            .From(rows)
            .AddColumn("Id", "Front", "Back")
            .WithTitle(title, ConsoleColor.DarkGreen)
            .ExportAndWriteLine();

        Console.WriteLine("\n\n");
    }
}
