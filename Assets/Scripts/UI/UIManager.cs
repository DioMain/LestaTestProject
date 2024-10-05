using UnityEngine;

public class UIManager : MonoBehaviour, IInitialize
{
    [SerializeField]
    private GameObject winTxt;
    [SerializeField]
    private GameObject loseTxt;
    [SerializeField] 
    private GameObject restartButton;

    [SerializeField]
    private LineBar lineBar;

    public void Initialize()
    {
        lineBar.Initialize();

        Life_OnDamage();

        winTxt.SetActive(false);
        loseTxt.SetActive(false);
        restartButton.SetActive(false);

        GameManager.Instance.Life.OnDamage += Life_OnDamage;
    }

    private void Life_OnDamage()
    {
        lineBar.SetValue(GameManager.Instance.Life.Heal / GameManager.Instance.Life.MaxHeal);
    }
}
