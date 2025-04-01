using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    public GameObject playerPrefab;
    public SceneLoader sceneLoader;
    public BattleStageController battleStageController;
    public ShopStageController shopStageController;
    public Dictionary<E_StageType, float> stagePoints;
    
    
    public ClawGame game;
    public int currentRound = 0;
    public Vector2 playerSpawnPosition;
    public List<E_StageType> selectedStages = new List<E_StageType>();
    public int basicStageDifficulty = 30;
    
    private int currentStageIndex = -1;
    private GameObject playerInstance;


    // private void Start()
    // {
    //     game.ClawStart();
    // }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStage();
        }
    }

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
        if (scene.name == "StageSelectScene") 
        {
            currentRound++;
        }
        else if (scene.name == "DontDestroy")
        {
        }
        else
        {
            ActivateCurrentStage();
        }
    }

    public int GetCurrentStageIndex()
    {
        return currentStageIndex;
    }
    
    public void NextStage()
    {
        currentStageIndex++;
        if (currentStageIndex >= selectedStages.Count)
        {
            sceneLoader.ReturnToStageSelectScene();
        }
        else
        {
            sceneLoader.LoadStage(selectedStages[currentStageIndex]);
        }
    }

    void ActivateCurrentStage()
    {
        E_StageType currentStageType = selectedStages[currentStageIndex];

        if (currentStageType == E_StageType.Battle)
        {
            battleStageController.gameObject.SetActive(true);
            shopStageController.gameObject.SetActive(false);
            battleStageController.StartBattleStage(playerPrefab);
        }
        else if (currentStageType == E_StageType.Shop)
        {
            battleStageController.gameObject.SetActive(false);
            shopStageController.gameObject.SetActive(true);
            // shopStageController.SetupShop();
        }
    }
    
    public void SelectStages()
    {
        selectedStages.Clear();
        List<E_StageType> availableStages = new List<E_StageType> { E_StageType.Battle, E_StageType.Shop };

        for (int i = 0; i < 3; i++)
        {
            E_StageType selectedStage = GetWeightedRandomStage(availableStages);
            selectedStages.Add(selectedStage);
        }
        currentStageIndex = -1;
    }

    private E_StageType GetWeightedRandomStage(List<E_StageType> stages)
    {
        float totalWeight = 0f;
        foreach (var stage in stages)
        {
            totalWeight += stagePoints.ContainsKey(stage) ? stagePoints[stage] : 0f;
        }
        
        
        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var stage in stages)
        {
            cumulativeWeight += stagePoints.ContainsKey(stage) ? stagePoints[stage] : 0f;
            if (randomValue < cumulativeWeight)
            {
                return stage;
            }
        }
        return stages[0]; // 예외 방지
    }

    
    private void ApplyStageSettings()
    {
        // 스테이지 배율에 따라 적들에게 적용,
        // 스테이지 매니저에서 하는것보다 게임 매니저 등에서 적용하는게 나을수도?
        // 게임매니저든 배틀매니저든 전투관련 처리하는 곳에서
        // 스테이지 배율 가져갈 수 있도록
        
        // 리워드는 스테이지 배율에 따라 가중치 부여
        // SetRewards();
    }
}