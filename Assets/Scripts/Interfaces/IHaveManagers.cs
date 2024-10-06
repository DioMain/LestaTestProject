public interface IHaveManagers
{
    public GameManager Game => GameManager.Instance;
    public LevelManager Level => LevelManager.Instance;
}