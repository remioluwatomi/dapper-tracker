using System.Data.SQLite;
using Dapper;


public class DBHelper
{
    private readonly string _connectionString;

    public DBHelper(string connectionString)
    {
        _connectionString = connectionString;
        CreateDB();
    }

    private void CreateDB()
    {
        try
        {
            using (SQLiteConnection connection = new(_connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS coding_tracker (
                Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                Date TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL
                )";

                connection.Execute(createTableQuery);
            }
        }
        catch (Exception err)
        {
            // CodingTrackerUtils.LogError("Can't connect to the data store at the moment:", err);
            Console.WriteLine(err.Message);
            Environment.Exit(0);
        }
    }


}
