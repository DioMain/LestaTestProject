using System;

public class CheckpointManager
{
    public Checkpoint CurrentCheckpoint { get; private set; }

    public event Action<Checkpoint> OnCheckpoint;
    
    public void SetCheckpoint(Checkpoint checkpoint)
    {
        CurrentCheckpoint = checkpoint;

        OnCheckpoint?.Invoke(checkpoint);
    }
}