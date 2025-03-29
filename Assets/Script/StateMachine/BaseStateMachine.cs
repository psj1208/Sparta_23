using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine 
{
    public IState CurrentState;

    public void ChangeState(IState state)
    {
        state?.Exit();
        CurrentState = state;
        state?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public abstract void StartAnimation(int animationHash);
    public abstract void StopAnimation(int animationHash);
}
