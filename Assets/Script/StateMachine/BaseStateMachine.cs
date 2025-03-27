using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine 
{
    protected IState CurrentState;

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
}
