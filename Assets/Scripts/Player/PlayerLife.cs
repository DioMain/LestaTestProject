using System;
using UnityEngine;

public class PlayerLife
{
    public float Heal { get; private set; }

    public event Action OnDamage;
    public event Action OnHealing;
    public event Action OnDeath;

    private GameConfig config;

    public PlayerLife(GameConfig config)
    {
        this.config = config;
    }

    public void Damage(float dmg)
    {
        Heal -= dmg;

        OnDamage?.Invoke();

        if (Heal <= 0)
            OnDeath?.Invoke();
    }

    public void Healing(float heal)
    {
        Heal = Mathf.Clamp(Heal + heal, 0, config.MaxHeal);

        OnHealing?.Invoke();
    }
}
