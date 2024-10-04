using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("Keys")]
    public KeyCode MoveUp = KeyCode.W;
    public KeyCode MoveLeft = KeyCode.A;
    public KeyCode MoveDown = KeyCode.S;
    public KeyCode MoveRight = KeyCode.D;
    [Space]
    public KeyCode Run = KeyCode.LeftShift;
    [Space]
    public KeyCode Jump = KeyCode.Space;

    [Header("Other")]
    public float MaxHeal = 100f;
}