using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public float CurrentHealth {  get; private set; }
    public float MaxHealth => statHandler.GetStat(EStatType.Health);
    public Action OnDamageAction;
    public Action OnDieAction;

    private Character character;
    private StatHandler statHandler;
    private Action<float, float> onChangeHealth;

    private void Awake()
    {
        character = GetComponent<Character>();
        statHandler = GetComponent<StatHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        AddChangeHealthEvent(character.CharacterStatUI.HpBar.UpdateHealth);
    }

    public void ChangeHealth(float amount)
    {
        CurrentHealth += amount;
        NotificationManager.Instance.ShowDamageIndicator(amount.ToString(), this.transform);
        onChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            onChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        }
        else if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDieAction?.Invoke();
            onChangeHealth?.Invoke(CurrentHealth, MaxHealth);
            // TODO : GameOver
            return;
        }

        if (amount < 0)
        {
            OnDamageAction?.Invoke();
        }
    }

    public void AddChangeHealthEvent(Action<float, float> onChange)
    {
        onChangeHealth += onChange;
        onChangeHealth?.Invoke(CurrentHealth, MaxHealth);
    }

    public void RemoveChangeHealthEvent(Action<float, float> onChange)
    {
        onChangeHealth -= onChange;
    }
}
