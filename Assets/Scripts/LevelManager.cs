using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public PlayerManager Player;
    public CameraCapture CameraCapture;
    public UIManager UI;
    public CheckpointManager Checkpoint;
    public GameTimer Timer;

    private void Start()
    {
        Instance = this;

        Cursor.visible = false;
        GameManager.Instance.Life.Restore();

        Timer = new GameTimer();

        Checkpoint = new CheckpointManager(this);

        Player.Initialize();
        UI.Initialize();
    }
}
