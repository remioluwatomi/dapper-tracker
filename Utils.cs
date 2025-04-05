using Spectre.Console;
using System.Globalization;

public static class CodingTrackerUtils
{
    public static void PauseApp()
    {
        AnsiConsole.MarkupLine("\n[bold yellow]Press Enter key to continue...[/]");

        Console.In.ReadLineAsync().GetAwaiter().GetResult();
    }

    public static DateTime ParseSessionDateTime(string dateString)
    {
        if (DateTime.TryParse(dateString, out DateTime date))
        {
            return date;
        }
        else
        {
            throw new Exception("Error parsing datetime from the DB..");
        }
    }

}