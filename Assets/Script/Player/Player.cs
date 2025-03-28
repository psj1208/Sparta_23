using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public ResourceController ResourceController;
    [HideInInspector] public StatHandler StatHandler;
    [HideInInspector] public Animator Animator;

    public PlayerStateMachine PlayerStateMachine;

    private void Awake()
    {
        ResourceController = GetComponent<ResourceController>();
        StatHandler = GetComponent<StatHandler>();
        Animator = GetComponentInChildren<Animator>();

        PlayerStateMachine = new PlayerStateMachine(this);
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }

    private void Update()
    {
        PlayerStateMachine.Update();

#if DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.AttackState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResourceController.ChangeHealth(-5);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResourceController.ChangeHealth(-10000);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log($"기존 공격력 : {StatHandler.GetStat(EStatType.Attack)}");
            StatHandler.ModifyStat(EStatType.Attack, 10, true, 0);
            Debug.Log($"최종 공격력 : {StatHandler.GetStat(EStatType.Attack)}");
            return;
        }
#endif
    }
}