using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(BaseStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StateMachine.StopAnimation((StateMachine as PlayerStateMachine).DamageAnimHash);
        StateMachine.StopAnimation((StateMachine as PlayerStateMachine).AttackAnimHash);
        StateMachine.StartAnimation((StateMachine as PlayerStateMachine).IdleAnimHash);
    }
}
