using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviourPlus
{
    [SerializeField]
    private GameObject winTxt;
    [SerializeField]
    private GameObject loseTxt;
    [SerializeField] 
    private GameObject restartButton;
    [SerializeField] 
    private GameObject checkpointInfo;
    [SerializeField]
    private TextMeshProUGUI timerOutput;

    [SerializeField]
    private LineBar lineBar;

    public override void Initialize()
    {
        lineBar.Initialize();

        Life_OnDamage();

        Game.Life.OnDamage += Life_OnDamage;
        Game.Life.OnDeath += Life_OnDeath;
        Game.Win.OnWin += On_Win;
        Level.Checkpoint.OnCheckpoint += Checkpoint_OnCheckpoint;
    }
    public void RestartGameButton() => SceneManager.LoadScene(0);

    private void On_Win()
    {
        if (!Game.Life.IsAlive)
            return;

        winTxt.SetActive(true);
        restartButton.SetActive(true);

        timerOutput.text = $"TIME: {Level.Timer.Time.Minutes}.{Level.Timer.Time.Seconds}.{Level.Timer.Time.Milliseconds}";
        timerOutput.gameObject.SetActive(true);
    }

    private void Life_OnDeath()
    {
        if (Game.Win.IsWin)
            return;

        loseTxt.SetActive(true);
        restartButton.SetActive(true);
    }

    private void Checkpoint_OnCheckpoint(Checkpoint obj)
    {
        StartCoroutine(CheckpointCoroutine());
    }

    private void Life_OnDamage()
    {
        lineBar.SetValue(Game.Life.Heal / Game.Life.MaxHeal);
    }

    private IEnumerator CheckpointCoroutine()
    {
        checkpointInfo.SetActive(true);

        yield return new WaitForSeconds(3f);

        checkpointInfo.SetActive(false);
    }

    private void OnDestroy()
    {
        Game.Life.OnDamage -= Life_OnDamage;
        Game.Life.OnDeath -= Life_OnDeath;
        Game.Win.OnWin -= On_Win;
        Level.Checkpoint.OnCheckpoint -= Checkpoint_OnCheckpoint;
    }
}
