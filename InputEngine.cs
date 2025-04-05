using Spectre.Console;

public class UserInput
{
    public delegate void PromptDelegate(string prompt, string format);

    
    public static int GetUserMenuOption()
    {
        string header = "Coding Track Menu";
        return GetIntMenuOption(header, PrintEngine.MenuOption, 4);
    }

    
    public static int GetOptionToUpdate()
    {
        string header = "Update Menu";
        return GetIntMenuOption(header, PrintEngine.UpdateOptions, 3);
    }


    private static int GetIntMenuOption(string header, string optionsBody, int maxOption, string promptQuery = "\nSelect an Option: ")
    {
        int result;
        while (true)
        {
            PrintEngine.PrintMenu(header, optionsBody);
            result = AnsiConsole.Prompt(new TextPrompt<int>(promptQuery));
            if (result < 0 || result > maxOption)
            {
                AnsiConsole.MarkupLine("\n[bold red]Invalid response..[/]");
                continue;
            }
            break;
        }
        return result;
    }


    public static int GetId(string action)
    {
        AnsiConsole.MarkupLine($"[bold yellow]Please enter the Id for the coding session to [red][/]{action}[/]:");
        PrintEngine.PrintMainMenuRequest();
        return AnsiConsole.Prompt(new TextPrompt<int>("Enter Id: ")); ;
    }


    private static string GetDateTimeWithPrompt(string prompt, string format, PromptDelegate promptDelegate)
    {
        promptDelegate(prompt, format);
        PrintEngine.PrintMainMenuRequest();
        return AnsiConsole.Prompt(new TextPrompt<string>(""));
    }


    public static string GetTime(string promptyQuery)
    {
        string inputTime;
        while (true)
        {
            inputTime = GetDateTimeWithPrompt(promptyQuery, PrintEngine.TimeFormatInstruction, PrintEngine.PrintDateTimePrompt);
            if (inputTime == "0") break;

            bool isValidTime = CodingTrackerUtils.IsValidTime(inputTime);
            if (!isValidTime)
            {
                AnsiConsole.MarkupLine("You may have entered an invalid time.. [italic underline](Time Format: hh:mm OR hh:mm:ss)[/]\n");
                continue;
            }
            break;
        }
        return inputTime;
    }


    public static string GetEndTime(string startTimeString, string endTimeQuery)
    {
        string inputEndTime;
        while (true)
        {
            inputEndTime = GetTime(endTimeQuery);
            if (inputEndTime == "0") break;
            if (TimeSpan.Parse(inputEndTime) < TimeSpan.Parse(startTimeString))
            {
                AnsiConsole.MarkupLine("[red bold]End Time cannot be less than the Start Time[/]");
                continue;
            }
            break;
        }
        return inputEndTime;
    }


    public static string GetDate()
    {
        string inputDate;
        while (true)
        {
            inputDate = GetDateTimeWithPrompt("Please enter the date for your coding session", PrintEngine.DateFormatInstruction, PrintEngine.PrintDateTimePrompt);

            if (inputDate == "0") break;

            bool isValidDte = CodingTrackerUtils.IsValidDate(inputDate);
            if (!isValidDte)
            {
                AnsiConsole.MarkupLine("You may have entered an invalid date.. [italic underline](Date Format: yyyy-mm-dd )[/]\n");
                continue;
            }

            int comparisonResult = DateTime.Compare(DateTime.Parse(inputDate), DateTime.Now);
            if (comparisonResult > 0)
            {
                AnsiConsole.MarkupLine("Date shouldn't be later than today..");
                continue;
            }

            break;
        }
        return inputDate;
    }
}