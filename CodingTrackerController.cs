using System;
using Spectre.Console;

public class CodingTackerController
{

    public static CodingSession? GetNewCodingSession()
    {
        // sometimes, maintaining simplicity is better than forcing DRY principles

        var dateInput = UserInput.GetDate();
        if (dateInput == "0") return null;

        var startTime = UserInput.GetTime("Please enter the START TIME for your coding session");
        if (startTime == "0") return null;

        var endTime = UserInput.GetEndTime(startTime, "\nPlease enter the END TIME for your coding session");
        if (endTime == "0") return null;

        CodingSession newCodingSession = new(DateTime.Parse(dateInput), DateTime.Parse(startTime), DateTime.Parse(endTime));

        return newCodingSession;

    }

    
    public static void ReadCodingSessions(DBHelper db)
    {
        var codingTracks = db.GetCodingTrack();

        PrintEngine.PrintTracks(codingTracks);
        CodingTrackerUtils.PauseApp();
    }


    public static void AddNewCodingSession(DBHelper db)
    {
        CodingSession? newCodingSession = GetNewCodingSession();

        if (newCodingSession == null) return;

        db.AddCodingTrack(newCodingSession);
        PrintEngine.PrintSuccessMessage("New session has been added to your history..");

        CodingTrackerUtils.PauseApp();
    }


    public static void DeleteCodingSession(DBHelper db)
    {
        int id = UserInput.GetId("DELETE");
        if (id == 0) return;

        db.DeleteCodingTrack(id);
        PrintEngine.PrintSuccessMessage("The identified coding session has been deleted");

        CodingTrackerUtils.PauseApp();
    }


    public static void UpdateCodingSession(DBHelper db)
    {
        int id = UserInput.GetId("UPDATE");
        if (id == 0) return;

        int optionToUpdate = UserInput.GetOptionToUpdate();
        if (optionToUpdate == 0) return;

        switch (optionToUpdate)
        {
            case 1:
                var dateInput = UserInput.GetDate();
                if (dateInput == "0") return;
                db.UpdateCodingTrack(id, "Date", DateTime.Parse(dateInput));
                PrintEngine.PrintSuccessMessage("Coding session  DATE has been updated");
                break;
            case 2:
                var startTime = UserInput.GetTime("Please enter the UPDATED START TIME for your coding session");
                if (startTime == "0") return;

                var endTime = UserInput.GetEndTime(startTime, "\nPlease enter the UPDATED END TIME for your coding session");
                if (endTime == "0") return;

                db.UpdateCodingTrack(id, DateTime.Parse(startTime), DateTime.Parse(endTime));

                PrintEngine.PrintSuccessMessage("Coding session time has been updated");
                break;
            case 3:
                CodingSession? newCodingSession = GetNewCodingSession();

                if (newCodingSession == null) return;

                db.UpdateCodingTrack(id, newCodingSession);
                PrintEngine.PrintSuccessMessage("Coding session has been updated");
                break;

            default:
                break;
        }
    }
}