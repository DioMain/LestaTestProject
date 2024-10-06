using System;
using UnityEngine;

public class PlayerLife
{
    public float Heal { get; private set; }

    public float MaxHeal => config.MaxHeal;

    public event Action OnDamage;
    public event Action OnHealing;
    public event Action OnDeath;

    private readonly GameConfig config;

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
        Heal = Mathf.Clamp(Heal + heal, 0, MaxHeal);

        OnHealing?.Invoke();
    }

    public void Restore() => Heal = MaxHeal;
}
