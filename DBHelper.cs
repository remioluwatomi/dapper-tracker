using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Configuration;
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
            CodingTrackerUtils.LogError("Can't connect to the data store at the moment:", err);
            Environment.Exit(0);
        }
    }


    public List<CodingSession> GetCodingTrack()
    {
        List<CodingSession> codingSessions = [];
        try
        {
            using (SQLiteConnection connection = new(_connectionString))
            {
                string query = "SELECT * FROM coding_tracker";
                codingSessions = connection.Query<CodingSession>(query).ToList();
            }
        }
        catch (Exception err)
        {
            CodingTrackerUtils.LogError("There was an error while fetching your coding track history:", err);
        }
        return codingSessions;
    }


    private void ProcessQuery(string query, object queryParams)
    {
        try
        {
            using (SQLiteConnection connection = new(_connectionString))
            {
                connection.Execute(query, queryParams);
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }


    public void AddCodingTrack(CodingSession session)
    {
        var sql = "INSERT INTO coding_tracker (Date, StartTime, EndTime) VALUES (@Date, @StartTime, @EndTime)";
        ProcessQuery(sql, session);
    }


    public void UpdateCodingTrack(int id, string col, DateTime value)
    {
        string updateQuery = $"UPDATE coding_tracker SET {col} = @Value WHERE Id = @Id;";
        ProcessQuery(updateQuery, new { Value = value, Id = id });
    }
    

    public void UpdateCodingTrack(int id, DateTime startTime, DateTime endTime)
    {
        string updateQuery = "UPDATE coding_tracker SET StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id;";
        ProcessQuery(updateQuery, new { StartTime = startTime, EndTime = endTime, Id = id });
    }


    public void UpdateCodingTrack(int id, CodingSession session)
    {
        string updateQuery = "UPDATE coding_tracker SET Date = @Date, StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id;";
        ProcessQuery(updateQuery, new { session.Date, session.StartTime, session.EndTime, Id = id });
    }


    public void DeleteCodingTrack(int id)
    {
        var dropQuery = "DELETE FROM coding_tracker WHERE Id = @Id;";
        ProcessQuery(dropQuery, new { Id = id });
    }
}
