public class CodingSession
{
    public int? Id { get; }
    public DateTime Date { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }

    public CodingSession(DateTime date, DateTime startTime, DateTime endTime)
    {
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }

    public CodingSession(int id, DateTime date, DateTime startTime, DateTime endTime)
    {
        Id = id;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }
}