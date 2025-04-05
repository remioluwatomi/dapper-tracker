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

        while (true)
        {
            int userInput = UserInput.GetUserMenuOption();
            ProcessSelectedMenu(userInput, db);
        }
    }


    public static void ProcessSelectedMenu(int selectedMenu, DBHelper db)
    {
        switch (selectedMenu)
        {
            case 0:
                CodingTrackerUtils.CloseApp();
                break;
            case 1:
                //View All Coding history
                try
                {
                    CodingTackerController.ReadCodingSessions(db);
                }
                catch (Exception e)
                {
                    CodingTrackerUtils.LogError("Can't read your coding session history at the moment..", e);
                }
                break;
            case 2:
                //Insert Record
                try
                {
                    CodingTackerController.AddNewCodingSession(db);
                }
                catch (Exception e)
                {
                    CodingTrackerUtils.LogError("Can't add to your coding session at the moment..", e);
                }

                break;
            case 3:
                try
                {
                    CodingTackerController.DeleteCodingSession(db);
                }
                catch (Exception e)
                {
                    CodingTrackerUtils.LogError("Can't delete coding session at the moment", e);
                }
                break;
            case 4:
                try
                {
                    CodingTackerController.UpdateCodingSession(db);
                }
                catch (Exception e)
                {
                    CodingTrackerUtils.LogError("can't update coding session at the moment", e);
                }
                break;
            default:
                AnsiConsole.MarkupLine($"[red bold]Invalid Menu Option Selected..[/]");
                break;
        }
    }

}

