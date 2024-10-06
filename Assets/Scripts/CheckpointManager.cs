using System;

public class CheckpointManager
{
    public Checkpoint CurrentCheckpoint { get; private set; }

    public event Action<Checkpoint> OnCheckpoint;

    private readonly LevelManager level;
    
    public CheckpointManager(LevelManager level)
    {
        this.level = level;
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        CurrentCheckpoint = checkpoint;

        OnCheckpoint?.Invoke(checkpoint);
    }

    public void SpawnPlayer()
    {
        if (CurrentCheckpoint != null)
            level.Player.transform.position = CurrentCheckpoint.transform.position;
    }
}