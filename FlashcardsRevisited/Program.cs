using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FlashcardsRevisited;
internal class Program
{
    static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    static void Main(string[] args)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = "SELECT @@VERSION";

        string version = command.ExecuteScalar().ToString();
        Console.WriteLine($"Connection successfull to {version}!");
        Console.ReadKey();
    }
}
