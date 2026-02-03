using Dapper;
using FlashcardsRevisited.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FlashcardsRevisited.Controllers;

internal class FlashcardController
{
    static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    internal List<Flashcard> GetAll()
    {
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var sql = "SELECTION * FROM Flashcards";

        return connection.Query<Flashcard>(sql).ToList();
    }

    internal Flashcard? GetById(int id)
    {
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var sql = "SELECTION * FROM Flashcards WHERE FlashcardId = @FlashcardId";

        return connection.QuerySingleOrDefault<Flashcard>(sql, id);
    }

    internal int Add(Flashcard flashcard)
    {
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var sql = "INSERT INTO Flashcards (Front, Back, StackId) VALUES (@Front, @Back, @StackId)";

        return connection.Execute(sql, new
        {
            Front = flashcard.Front,
            Back = flashcard.Back,
            StackId = flashcard.Stack.StackId,
        });
    }

    internal int Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var sql = "DELETE FROM Flashcards WHERE FlashcardId = @DeleteId";

        return connection.Execute(sql, new
        {
            DeleteId = id
        });
    }

    internal int Update(Flashcard newFlashcard)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var sql = @"UPDATE FROM Flashcards 
                    SET Front = @Front, Back = @Back
                    WHERE FlashcardId = @FlashcardId";

        return connection.Execute(sql, new
        {
            Front = newFlashcard.Front,
            Back = newFlashcard.Back,
            FlashcardId = newFlashcard.FlashcardId,
        });
    }
}
