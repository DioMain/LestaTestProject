using System;

public class WinManager
{
    public bool IsWin { get; private set; }

    public Action OnWin;

    public WinManager()
    {
        IsWin = false;
    }

    public void Win()
    {
        OnWin?.Invoke();
    }
}