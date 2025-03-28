using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    private PlayerStateMachine playerStateMachine;
    private float attackInterval = 0.7f;
    private float lastAttackTime = 0;

    public PlayerAttackState(BaseStateMachine stateMachine) : base(stateMachine)
    {
        playerStateMachine = stateMachine as PlayerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // attackInterval 초를 간격으로 공격 실행
        if(Time.time - lastAttackTime < attackInterval)
        {
            lastAttackTime = Time.time;
            playerStateMachine.StartAnimation(playerStateMachine.AttackAnimHash);

            if (!playerStateMachine.Player.ExecuteItem())
            {
                playerStateMachine.StopAnimation(playerStateMachine.AttackAnimHash);
                playerStateMachine.ChangeState(playerStateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
