using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    #region Enemy AnimationData
    private string attackParameterName = "Attack";
    private string damageParameterName = "Damage";
    private string dieParameterName = "Die";
    public int AttackAnimHash { get; private set; }
    public int DamageAnimHash { get; private set; }
    public int DieAnimHash { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Animator = GetComponent<Animator>();
        ResourceController = GetComponent<ResourceController>();
        StatHandler = GetComponent<StatHandler>();

        AttackAnimHash = Animator.StringToHash(attackParameterName);
        DamageAnimHash = Animator.StringToHash(damageParameterName);
        DieAnimHash = Animator.StringToHash(dieParameterName);
    }

    protected override void Start()
    {
        base.Start();
        ResourceController.OnDamageAction += Damaged;
        ResourceController.OnDieAction += Die;

        TurnManager.Instance.OnEnemyTurnStart += AttackOnce;
    }

#if DEBUG
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ResourceController.ChangeHealth(-5);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResourceController.ChangeHealth(-10000);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AttackOnce(GameManager.Instance.Player);
        }
    }
#endif

    /// <summary>
    /// Enemy의 공격 함수
    /// </summary>
    public void AttackOnce(Player player)
    {
        TriggerAnimation(AttackAnimHash);
        player.ResourceController.ChangeHealth(-StatHandler.GetStat(EStatType.Attack));
    }

    /// <summary>
    /// Enemy 피격
    /// </summary>
    public void Damaged()
    {
        TriggerAnimation(DamageAnimHash);
    }

    /// <summary>
    /// Enemy 사망
    /// </summary>
    public void Die()
    {
        TriggerAnimation(DieAnimHash);
    }

    void TriggerAnimation(int animationHash)
    {
        Animator.SetTrigger(animationHash);
    }
}
