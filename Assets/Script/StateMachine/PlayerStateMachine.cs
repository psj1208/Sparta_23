using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    public Player Player;
    public List<Enemy> curEnemies;

    #region State
    public PlayerIdleState IdleState {  get; set; }
    public PlayerBattleState BattleState { get; set; }
    #endregion

    #region AnimationData
    private string idleParameterName = "IsIdle";
    private string attackParameterName = "IsAttack";
    private string damageParameterName = "IsDamage";
    private string dieParameterName = "IsDie";
    public int IdleAnimHash { get; private set; }
    public int AttackAnimHash { get; private set; }
    public int DamageAnimHash { get; private set; }
    public int DieAnimHash { get; private set; }
    #endregion
    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        BattleState = new PlayerBattleState(this);

        IdleAnimHash = Animator.StringToHash(idleParameterName);
        AttackAnimHash = Animator.StringToHash(attackParameterName);
        DamageAnimHash = Animator.StringToHash(damageParameterName);
        DieAnimHash = Animator.StringToHash(dieParameterName);
    }

    public override void StartAnimation(int animationHash)
    {
        this.Player.Animator.SetBool(animationHash, true);
    }

    public override void StopAnimation(int animationHash)
    {
        this.Player.Animator.SetBool(animationHash, false);
    }
}
