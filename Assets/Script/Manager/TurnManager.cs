using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : Singleton<TurnManager>
{
    public event Action<List<Enemy>> OnPlayerTurnStart; // 플레이어 턴 시작 이벤트
    public event Action<Player> OnEnemyTurnStart; // 적 턴 시작 이벤트
    public event Action OnClawMachineStart; // 뽑기 시작 이벤트
    public event Action OnTurnChanged; // 턴 변경 이벤트

    private ETurnState currentState;
    public Player player;
    public List<Enemy> currentEnemies;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            GetEnemyListFromStageManager();
        }
        else if (scene.name == "DontDestroy")
        {
        }
        else
        {
        }
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            StartClawMachine();
            ApplySkills();
        }
    }

    private void ApplySkills()
    {
        foreach (var skill in  ItemInventoryManager.Instance.skills)
        {
            skill.UseSkill(player);
        }
       
    }

    private void GetEnemyListFromStageManager()
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }

        currentEnemies = StageManager.Instance.battleStageController.spawnedEnemies;
    }
    
    private void StartPlayerTurn()
    {
        Debug.Log("StartPlayerTurn");
        currentState = ETurnState.PlayerTurn;
        OnTurnChanged?.Invoke();
        OnPlayerTurnStart?.Invoke(currentEnemies);
    }

    private void StartEnemyTurn()
    {
        currentState = ETurnState.EnemyTurn;
        OnTurnChanged?.Invoke();

        StartCoroutine(EnemyAction());
    }

    
    private IEnumerator EnemyAction()
    {
        yield return new WaitForSeconds(1.5f);
        if (currentEnemies.Count <= 0)
        {
            yield break;
        }
        OnEnemyTurnStart?.Invoke(GameManager.Instance.Player);

        yield return new WaitForSeconds(1.0f);
        StartClawMachine(); 
    }

    private void StartClawMachine()
    {
        Debug.Log("Starting Claw Machine");
        currentState = ETurnState.ClawTurn;
        OnTurnChanged?.Invoke();
        OnClawMachineStart?.Invoke();
        ItemInventoryManager.Instance.itemSpawner.SpawnInventoryItems();
        if (GameManager.Instance.Player != null)
            GameManager.Instance.Player.ReduceItemsTurn();
    }

    public void EndPlayerTurn()
    {
        if (currentState != ETurnState.PlayerTurn) return;
        OnTurnChanged?.Invoke();
        GameManager.Instance.Player.EndBattleTurn();
        StartEnemyTurn();
    }

    public void EndClawMachine()
    {
        if (currentState != ETurnState.ClawTurn) return;
        OnTurnChanged?.Invoke();
        StartPlayerTurn();
    }
}
