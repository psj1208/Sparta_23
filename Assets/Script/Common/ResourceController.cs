using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [HideInInspector] public StatHandler StatHandler;

    public float CurrentHealth {  get; private set; }
    public float MaxHealth => StatHandler.GetStat(EStatType.Health);

    private Action<float, float> OnChangeHealth;

    public void ChangeHealth(float amount)
    {
        CurrentHealth += amount;
        if(CurrentHealth < MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else if(CurrentHealth < 0)
        {
            CurrentHealth = 0;
            // TODO : Death 
        }

        OnChangeHealth?.Invoke(CurrentHealth, amount);
    }

    public void AddChangeHealthEvent(Action<float, float> onChange)
    {
        OnChangeHealth += onChange;
    }

    public void RemoveChangeHealthEvent(Action<float, float> onChange)
    {
        OnChangeHealth -= onChange;
    }
}
