using Dapper;
using Microsoft.Data.SqlClient;

namespace FlashcardsRevisited.Services;
internal class DatabaseManager
{
    internal void CreateTable(string connectionString)
    {
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var sqlStack = @"IF OBJECT_ID('dbo.Stack', 'U') IS NULL
            BEGIN
                CREATE TABLE Stack (
                    StackId INT IDENTITY NOT NULL,
                    StackName VARCHAR(25) NOT NULL,
                    Description VARCHAR(255),
                    CreatedDate DATE,

                    CONSTRAINT PK_Stack PRIMARY KEY (StackId),
                    CONSTRAINT UQ_StackName UNIQUE (StackName)
                );
            END";

        connection.Execute(sqlStack);

        var sqlFlashcards = @"IF OBJECT_ID('dbo.Flashcards', 'U') IS NULL
            BEGIN
                CREATE TABLE Flashcards (
                    FlashcardsId INT IDENTITY NOT NULL,
                    Front VARCHAR NOT NULL,
                    Back VARCHAR NOT NULL,
                    StackId INT NOT NULL,

                    CONSTRAINT PK_Flashcards PRIMARY KEY (FlashcardsId),
                    CONSTRAINT FK_Flashcards_StackId FOREIGN KEY (StackId)
                        REFERENCES Stack(StackId)
                        ON DELETE CASCADE
                    );
            END";

        connection.Execute(sqlFlashcards);
    }
}
