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

    protected override void Start()
    {
        base.Start();
        ResourceController.OnDamageAction += DamageAction;
        ResourceController.OnDieAction += DieAction;

        TurnManager.Instance.OnPlayerTurnStart -= StartBattleTurn;
        TurnManager.Instance.OnPlayerTurnStart += StartBattleTurn;

        StatHandler.OnAtkUpdate += CharacterStatUI.HpBar.UpdateAdditionalAtk;
        StatHandler.OnDefUpdate += CharacterStatUI.HpBar.UpdateShield;
        StatHandler.OnAtkUpdate((int)StatHandler.GetStat(EStatType.Attack));
        StatHandler.OnDefUpdate((int)StatHandler.GetStat(EStatType.Defense));
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
    /// <param name="enemy">현재 상대하는 몬스터</param>
    public void StartBattleTurn(List<Enemy> enemy)
    {
        ReduceItemsTurn();
        PlayerStateMachine.curEnemies = enemy;
        PlayerStateMachine.ChangeState(PlayerStateMachine.BattleState);
    }

    private void ReduceItemsTurn()
    {
        if(StatHandler.Turns?.Count > 0)
        {
            for(int i = 0; i < StatHandler.Turns.Count; i++)
            {
                StatHandler.Turns[i] -= 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.TryGetComponent<IItem>(out IItem item))
        {
            this.CurItem = item;
            AudioManager.Instance.PlaySFX(ESFXType.GoodEffect);
        }
    }

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
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlaySFX(ESFXType.Damaged);
        PlayerStateMachine.StartAnimation(PlayerStateMachine.DamageAnimHash);
        yield return null;
        PlayerStateMachine.StopAnimation(PlayerStateMachine.DamageAnimHash);
    }    
}