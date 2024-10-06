using UnityEngine;

public class PlayerManager : MonoBehaviourPlus
{
    public PlayerMovement Movement;

    public override void Initialize()
    {
        Movement.Initialize();

        Game.Life.OnDeath += EndGameEffect;
        Game.Win.OnWin += EndGameEffect;
    }

    private void EndGameEffect()
    {
        Movement.CanJump = false;
        Movement.CanMove = false;
        Movement.CanRotate = false;

        Level.CameraCapture.CanCapture = false;

        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        Game.Life.OnDeath -= EndGameEffect;
        Game.Win.OnWin -= EndGameEffect;
    }
}
