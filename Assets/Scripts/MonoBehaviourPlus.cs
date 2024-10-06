using UnityEngine;

public abstract class MonoBehaviourPlus : MonoBehaviour, IInitialize
{
    protected GameManager Game => GameManager.Instance;
    protected LevelManager Level => LevelManager.Instance;

    public virtual void Initialize() { }
}