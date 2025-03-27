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
        // TODO : 플레이어 애니메이션을 Attack으로 변경
        Debug.Log("어택");
    }
    public override void Update()
    {
        base.Update();
        // TODO : 공격 캡슐 남아있는 동안 지속 
    }
}
