using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        Debug.Log(GameManager.Instance.Life.Heal);
        Debug.Log(GameManager.Instance.Life.MaxHeal);
        lineBar.SetValue(GameManager.Instance.Life.Heal / GameManager.Instance.Life.MaxHeal);
    }
}
