using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    private PlayerStateMachine playerStateMachine;

    public PlayerAttackState(BaseStateMachine stateMachine) : base(stateMachine)
    {
        playerStateMachine = stateMachine as PlayerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        playerStateMachine.StartAnimation(playerStateMachine.AttackAnimHash);
        playerStateMachine.Player.CurItem?.UseItem();
        // TODO : 실질적 데미지 적용
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
