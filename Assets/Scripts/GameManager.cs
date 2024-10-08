using UnityEngine;

public class GameManager : MonoBehaviour, IInitialize
{
    public static GameManager Instance;

    public GameConfig Config;
    public PlayerLife Life;
    public WinManager Win;
    public AudioManager Audio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Initialize();
        }
        else
            Destroy(gameObject);
    }

    public void Initialize()
    {
        Config = Resources.Load<GameConfig>("Config");

        Win = new WinManager();

        Life = new PlayerLife(Config);
        Life.Restore();
    }
}
