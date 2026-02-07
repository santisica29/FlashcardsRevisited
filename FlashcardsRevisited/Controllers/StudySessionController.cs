using Dapper;
using FlashcardsRevisited.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FlashcardsRevisited.Controllers;
internal class StudySessionController
{
    static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    internal int Add(StudySession session)
    {
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var sql = @"INSERT INTO StudyArea (Score, DateOfSession, StackId) VALUES (@Score, @DateOfSession, @StackId)";

        return connection.Execute(sql, new
        {
            Score = session.Score,
            DateOfSession = session.DateOfSession,
            StackId = session.StackId,
        });
    }
}
