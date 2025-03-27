using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public ResourceController ResourceController;
    public Animator Animator;

    private PlayerStateMachine stateMachine;
    private void Awake()
    {
        ResourceController = GetComponent<ResourceController>();
        Animator = GetComponentInChildren<Animator>();

        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();

        if(Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}