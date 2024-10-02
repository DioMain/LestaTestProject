using System.Collections;
using System.Collections.Generic;
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
        Config = Resources.Load<GameConfig>("Config");
    }
}
