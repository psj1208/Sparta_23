using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    public PlayerAttackState(BaseStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StateMachine.StartAnimation((StateMachine as PlayerStateMachine).AttackAnimHash);
    }
    public override void Update()
    {
        base.Update();
        // TODO : 공격 캡슐 남아있는 동안 공격
    }

    public override void Exit()
    {
        base.Exit();
        StateMachine.StopAnimation((StateMachine as PlayerStateMachine).AttackAnimHash);
    }
}
