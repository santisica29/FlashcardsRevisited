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

        var sql = $"INSERT INTO Stack (StackId, StackName, Description, CreatedDate) VALUES (@StackId, @StackName, @Description, @CreatedDate)";

        return connection.Execute(sql, new
        {
            stack.StackId,
            stack.StackName,
            stack.Description,
            stack.CreatedDate,
        });
    }

    internal int Update()
}
