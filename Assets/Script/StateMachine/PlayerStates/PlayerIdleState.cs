using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    private PlayerStateMachine playerStateMachine;
    public PlayerIdleState(BaseStateMachine stateMachine) : base(stateMachine)
    {
        playerStateMachine = stateMachine as PlayerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.StopAnimation(playerStateMachine.DamageAnimHash);
        playerStateMachine.StopAnimation(playerStateMachine.AttackAnimHash);
        playerStateMachine.StartAnimation(playerStateMachine.IdleAnimHash);
    }
}
