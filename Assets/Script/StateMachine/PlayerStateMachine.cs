using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    Player player;

    #region State
    public PlayerIdleState IdleState {  get; set; }
    public PlayerAttackState AttackState { get; set; }
    #endregion

    #region AnimationData
    private string attackParameterName = "IsAttack";
    private string damageParameterName = "IsDamage";
    private string dieParameterName = "IsDie";
    public int AttackAnimHash { get; private set; }
    public int DamageAnimHash { get; private set; }
    public int DieAnimHash { get; private set; }
    #endregion
    public PlayerStateMachine(Player player)
    {
        this.player = player;

        IdleState = new PlayerIdleState(this);
        AttackState = new PlayerAttackState(this);

        AttackAnimHash = Animator.StringToHash(attackParameterName);
        DamageAnimHash = Animator.StringToHash(damageParameterName);
        DieAnimHash = Animator.StringToHash(dieParameterName);
    }

    public void StartAnimation(int animationHash)
    {
        this.player.Animator.SetBool(animationHash, true);
    }

    public void StopAnimation(int animationHash)
    {
        this.player.Animator.SetBool(animationHash, false);
    }
}
