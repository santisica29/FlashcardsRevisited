using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Configuration;
using FlashcardsRevisited.Models;

namespace FlashcardsRevisited.Controllers;
internal class StackController
{
    static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    internal int Add(StackDeck stack)
    {
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var sql = @"INSERT INTO Stacks (StackId, StackName, Description, CreatedDate) VALUES (@StackId, @StackName, @Description, @CreatedDate)";

        return connection.Execute(sql, new
        {
            stack.StackId,
            stack.StackName,
            stack.Description,
            stack.CreatedDate,
        });
    }

    internal int Update(StackDeck newStackDeck)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var sql = @"UPDATE Stacks 
                    SET StackName = @NewStackName, Description = @NewDescription
                    WHERE StackId = @StackId";

        return connection.Execute(sql, new
        {
            newStackDeck.StackName,
            newStackDeck.Description,
            newStackDeck.StackId,
        });
    }

    internal int Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var sql = @"DELETE FROM Stacks WHERE StackId = @StackId";

        return connection.Execute(sql, id);
    }

    internal StackDeck? GetById(int id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var sql = @"SELECT * FROM Stacks
                    WHERE StackId = @StackId";

        return connection.QuerySingleOrDefault<StackDeck>(sql, id);
    }

    internal List<StackDeck> GetAll()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var sql = "SELECT * FROM Stacks";

        return connection.Query<StackDeck>(sql).ToList();
    }
}
