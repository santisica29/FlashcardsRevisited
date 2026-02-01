using FlashcardsRevisited.Services;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FlashcardsRevisited;
internal class Program
{
    static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    static void Main(string[] args)
    {
        DatabaseManager databaseManager = new();

        databaseManager.CreateTable(connectionString);

        Console.WriteLine($"Connection successfull!");
        Console.ReadKey();
    }
}
