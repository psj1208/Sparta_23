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
        // Debug.Log(IsAnimationEnd);
        // Debug.Log(playerStateMachine.Player.CurItem);
        // Debug.Log(playerStateMachine.curEnemies);
        if (!IsAnimationEnd && playerStateMachine.Player.CurItem != null && playerStateMachine.curEnemies != null)
        {
            // 아이템 수치 적용
            playerStateMachine.Player.CurItem.UseItem(playerStateMachine.Player);
            playerStateMachine.Player.CurItem = null;

            // 실질적 데미지 적용
            foreach(Enemy enemy in playerStateMachine.curEnemies)
            {
                if(enemy != null)
                {
                    playerStateMachine.StartAnimation(playerStateMachine.AttackAnimHash);
                    enemy?.ResourceController.ChangeHealth(playerStateMachine.Player.GetAttackDamage());
                }
            }
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
