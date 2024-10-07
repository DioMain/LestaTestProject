using System;

public class GameTimer
{
    private DateTime startTime;
    private DateTime endTime;

    public TimeSpan Time => endTime - startTime;

    public void StartWatch()
    {
        startTime = DateTime.Now;
    }

    public void StopWatch()
    {
        endTime = DateTime.Now;
    }
}