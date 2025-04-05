using Spectre.Console;


public static class PrintEngine
{
    public static readonly string DateFormatInstruction = $"[bold yellow]in this format:[/] [underline green]yyyy-MM-dd[/] (e.g., [italic]2024-11-01[/]).";

    public static readonly string TimeFormatInstruction = $"[bold yellow]in this format:[/] [underline green]hh:mm OR hh:mm:ss[/] (e.g., [italic]14:30:00[/]).";

    public static readonly string MenuOption = @"
        0. ENTER '0' to close the app
        1. ENTER '1' to view your coding session history
        2. ENTER '2' to add a coding session
        3. ENTER '3' to delete from your coding session history
        4. ENTER '4' to update your coding history
        ";

    public static readonly string UpdateOptions = @"
        0. ENTER '0' to return to the main menu
        1. ENTER '1' to Update the sesion Date
        2. ENTER '2' to Update the session Start time and End Time
        3. ENTER '3' to update all values in the coding session
    ";
    

    public static void PrintWelcomeMessage()
    {
        AnsiConsole.Write(
        new FigletText("Welcome To CodeTrack")
            .LeftJustified()
            .Color(Color.Red));

        AnsiConsole.MarkupLine("\n[bold cyan]Hello, Coder![/][underline blue]Track Your Coding Journey![/]");
    }


    private static void EnginePanel(string header, string content)
    {
        var menuPanel = new Panel($"[teal]{content}[/]")
        {
            Header = new PanelHeader(header),
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(menuPanel);
    }


    public static void PrintMenu(string header, string menuString)
    {
        EnginePanel(header, menuString);
    }


    public static void PrintTracks(List<CodingSession> codingTracks)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Date");
        table.AddColumn("StartTime");
        table.AddColumn("EndTime");
        table.AddColumn("Duration");

        foreach (CodingSession codingTrack in codingTracks)
        {
            //will implement the method for this..
        }
        AnsiConsole.Write(table);
    }


    public static void PrintDateTimePrompt(string prompt, string format)
    {
        AnsiConsole.MarkupLine($"[bold yellow]{prompt}[/] {format}");
    }


    public static void PrintMainMenuRequest()
    {
        AnsiConsole.MarkupLine("To go back to the main menu, simply enter [bold red]0[/].");
    }


    public static void PrintSuccessMessage(string successMessage)
    {
        AnsiConsole.MarkupLine($"\n[bold green]{successMessage}[/]\n");
    }

}