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


    public static void LogError(string customMessage, Exception e)
    {
        AnsiConsole.MarkupLine($"[underline italic red]{customMessage}\n[/]");
        AnsiConsole.MarkupLine($"[]{e}[/]");
    }


    public static void CloseApp()
    {
        AnsiConsole.MarkupLine("\n[bold red]Hate to see you leave...[/]");
        Environment.Exit(0);
    }


    public static bool IsValidTime(string timeString)
    {
        try
        {
            return TimeSpan.TryParse(timeString, out TimeSpan time);
        }
        catch (Exception e)
        {
            LogError("There was an error processing the date.. try again", e);
            throw;
        }
    }


    public static bool IsValidDate(string dateString)
    {
        try
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            return DateTime.TryParseExact(dateString, "yyyy-MM-dd", culture, DateTimeStyles.None, out DateTime convertedDate);
        }
        catch (Exception e)
        {
            LogError("There was an error processing the date.. try again", e);
            throw;
        }
    }

}