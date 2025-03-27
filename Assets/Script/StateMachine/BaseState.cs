using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : IState
{
    public BaseStateMachine StateMachine;
    public BaseState(BaseStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
       
    }

    public virtual void Update()
    {
        
    }
}
