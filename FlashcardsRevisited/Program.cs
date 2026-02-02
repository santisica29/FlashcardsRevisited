using FlashcardsRevisited.Services;
using System.Configuration;

namespace FlashcardsRevisited;
internal class Program
{
    internal static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    static void Main(string[] args)
    {
        DatabaseManager databaseManager = new();

        databaseManager.CreateTable(connectionString);

        Console.WriteLine($"Connection successfull!");
        Console.ReadKey();
    }
}
