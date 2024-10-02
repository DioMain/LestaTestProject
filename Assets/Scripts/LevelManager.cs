using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public PlayerManager Player;
    public CameraCapture CameraCapture;

    private void Start()
    {
        Instance = this;
    }
}
