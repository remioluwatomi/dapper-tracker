using System;
using System.Configuration;
using Spectre.Console;


class Program
{
    public static void Main()
    {
        // dotnet add package Spectre.Console

        string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine($"Connection string is empty");
            Environment.Exit(0);
        }

        DBHelper db = new(connectionString);

        PrintEngine.PrintWelcomeMessage();
        CodingTrackerUtils.PauseApp();
    }

}

