using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public PlayerManager Player;
    public CameraCapture CameraCapture;
    public UIManager UI;
    public CheckpointManager Checkpoint;

    private void Start()
    {
        Instance = this;

        Player.Initialize();
        UI.Initialize();

        Checkpoint = new CheckpointManager();
    }
}
