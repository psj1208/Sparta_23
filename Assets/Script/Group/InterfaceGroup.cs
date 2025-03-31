using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string ItemName { get; }
    void UseItem(Player player);
}

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
}
