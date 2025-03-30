using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerStateMachine PlayerStateMachine;

    public IItem CurItem;

    private Coroutine DamageCoroutine;

    protected override void Awake()
    {
        base.Awake();
        Animator = GetComponentInChildren<Animator>();
        ResourceController = GetComponent<ResourceController>();
        StatHandler = GetComponent<StatHandler>();

        PlayerStateMachine = new PlayerStateMachine(this);
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }

    private void Start()
    {
        ResourceController.OnDamageAction += DamageAction;
        ResourceController.OnDieAction += DieAction;
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
            PlayerStateMachine.ChangeState(PlayerStateMachine.BattleState);
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

    /// <summary>
    /// BattleState로 전환.
    /// </summary>
    public void StartBattleTurn()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.BattleState);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<IItem>(out item))
    //    {
    //        // this.CurItem = item;
    //    }
    //}

    void DamageAction()
    {
        if(DamageCoroutine != null)
        {
            StopCoroutine(DamageCoroutine);
        }
        DamageCoroutine = StartCoroutine(DamageAnimation());
    }

    void DieAction()
    {
        PlayerStateMachine.StartAnimation(PlayerStateMachine.DieAnimHash);
    }

    IEnumerator DamageAnimation()
    {
        PlayerStateMachine.StartAnimation(PlayerStateMachine.DamageAnimHash);
        yield return null;
        PlayerStateMachine.StopAnimation(PlayerStateMachine.DamageAnimHash);
    }
}