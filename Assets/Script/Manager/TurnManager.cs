using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{

    public event Action OnPlayerTurnStart; // 플레이어 턴 시작 이벤트
    public event Action OnEnemyTurnStart; // 적 턴 시작 이벤트
    public event Action OnClawMachineStart; // 뽑기 시작 이벤트
    public event Action OnTurnChanged; // 턴 변경 이벤트

    private enum TurnState { PlayerTurn, EnemyTurn, ClawMachine }
    private TurnState currentState;


    private void Start()
    {
        StartClawMachine();
    }
    
    private void StartPlayerTurn()
    {
        currentState = TurnState.PlayerTurn;
        OnTurnChanged?.Invoke();
        OnPlayerTurnStart?.Invoke();
    }

    private void StartEnemyTurn()
    {
        currentState = TurnState.EnemyTurn;
        OnTurnChanged?.Invoke();
        OnEnemyTurnStart?.Invoke();

        StartCoroutine(EnemyAction());
    }

    
    private IEnumerator EnemyAction()
    {
        yield return new WaitForSeconds(1.5f); // 적의 행동 딜레이

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.ResourceController.ChangeHealth(-10);
        }

        yield return new WaitForSeconds(1.0f);
        StartClawMachine(); 
    }

    private void StartClawMachine()
    {
        currentState = TurnState.ClawMachine;
        OnTurnChanged?.Invoke();
        OnClawMachineStart?.Invoke();
    }

    public void EndPlayerTurn()
    {
        if (currentState != TurnState.PlayerTurn) return;
        OnTurnChanged?.Invoke();
        StartEnemyTurn();
    }

    public void EndClawMachine()
    {
        if (currentState != TurnState.ClawMachine) return;
        OnTurnChanged?.Invoke();
        StartPlayerTurn();
    }
}
