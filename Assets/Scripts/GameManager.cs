using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour, IInitialize
{
    public static GameManager Instance;

    public GameConfig Config;

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
    }
}
