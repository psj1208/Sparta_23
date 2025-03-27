using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    abstract void Use();
}

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
}
