using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : IState
{
    public BaseStateMachine StateMachine;
    protected bool IsAnimationEnd;
    public BaseState(BaseStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }
    public virtual void Enter()
    {
        IsAnimationEnd = false;
    }

    public virtual void Exit()
    {
       
    }

    public virtual void Update()
    {
        
    }

    public void AnimationEndEvent()
    {
        IsAnimationEnd = true;
    }
}
