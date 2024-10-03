using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    public KeyCode MoveUp = KeyCode.W;
    public KeyCode MoveLeft = KeyCode.A;
    public KeyCode MoveDown = KeyCode.S;
    public KeyCode MoveRight = KeyCode.D;

    public KeyCode Run = KeyCode.LeftShift;

    public KeyCode Jump = KeyCode.Space;
}