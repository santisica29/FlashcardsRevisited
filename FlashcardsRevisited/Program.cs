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
        Console.WriteLine("Connection successfull!");
        Console.ReadKey();
    }
}
