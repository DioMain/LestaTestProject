using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour, IInitialize
{
    public static GameManager Instance;

    public GameConfig Config;
    public PlayerLife Life;

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
        Cursor.visible = false;

        Config = Resources.Load<GameConfig>("Config");

        Life = new PlayerLife(Config);
    }
}
