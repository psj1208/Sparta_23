using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleState : BaseState
{
    private PlayerStateMachine playerStateMachine;

    public PlayerBattleState(BaseStateMachine stateMachine) : base(stateMachine)
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

        #if DEBUG
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            playerStateMachine.StartAnimation(playerStateMachine.AttackAnimHash);
        }
        #endif

        if (!IsAnimationEnd && playerStateMachine.Player.CurItem != null)
        {
            playerStateMachine.Player.CurItem?.UseItem();
            playerStateMachine.Player.CurItem = null;
            playerStateMachine.StartAnimation(playerStateMachine.AttackAnimHash);
            // TODO : 실질적 데미지 적용
        }

        if (IsAnimationEnd)
        {
            IsAnimationEnd = false;
            playerStateMachine.StopAnimation(playerStateMachine.AttackAnimHash);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
