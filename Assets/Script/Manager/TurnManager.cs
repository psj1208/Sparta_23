using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{

    public event Action<List<Enemy>> OnPlayerTurnStart; // 플레이어 턴 시작 이벤트
    public event Action<Player> OnEnemyTurnStart; // 적 턴 시작 이벤트
    public event Action OnClawMachineStart; // 뽑기 시작 이벤트
    public event Action OnTurnChanged; // 턴 변경 이벤트

    private ETurnState currentState;


    private void Start()
    {
        StartClawMachine();
    }
    
    private void StartPlayerTurn()
    {
        currentState = ETurnState.PlayerTurn;
        OnTurnChanged?.Invoke();
        OnPlayerTurnStart?.Invoke();
    }

    private void StartEnemyTurn()
    {
        currentState = ETurnState.EnemyTurn;
        OnTurnChanged?.Invoke();
        OnEnemyTurnStart?.Invoke();

        StartCoroutine(EnemyAction());
    }

    
    private IEnumerator EnemyAction()
    {
        yield return new WaitForSeconds(1.5f); 

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
        currentState = ETurnState.ClawTurn;
        OnTurnChanged?.Invoke();
        OnClawMachineStart?.Invoke();
    }

    public void EndPlayerTurn()
    {
        if (currentState != ETurnState.PlayerTurn) return;
        OnTurnChanged?.Invoke();
        StartEnemyTurn();
    }

    public void EndClawMachine()
    {
        if (currentState != ETurnState.ClawTurn) return;
        OnTurnChanged?.Invoke();
        StartPlayerTurn();
    }
}
