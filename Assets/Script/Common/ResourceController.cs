using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public float CurrentHealth {  get; private set; }
    public float MaxHealth => statHandler.GetStat(EStatType.Health);

    private Player player;
    private StatHandler statHandler;
    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        player = GetComponent<Player>();
        statHandler = GetComponent<StatHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void ChangeHealth(float amount)
    {
        if (amount < 0) StartCoroutine(DamageAnimation());
        CurrentHealth += amount;


        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else if(CurrentHealth < 0)
        {
            CurrentHealth = 0;

            // TODO : GameOver
            player.PlayerStateMachine.StartAnimation(player.PlayerStateMachine.DieAnimHash);
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

    IEnumerator DamageAnimation()
    {
        player.PlayerStateMachine.StartAnimation(player.PlayerStateMachine.DamageAnimHash);
        yield return null;
        player.PlayerStateMachine.StopAnimation(player.PlayerStateMachine.DamageAnimHash);
    }
}
