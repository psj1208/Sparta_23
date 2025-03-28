using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //public EnemyStateMachine EnemyStateMachine;


    protected override void Awake()
    {
        base.Awake();
        Animator = GetComponentInChildren<Animator>();
        ResourceController = GetComponent<ResourceController>();
        StatHandler = GetComponent<StatHandler>();
    }
}
