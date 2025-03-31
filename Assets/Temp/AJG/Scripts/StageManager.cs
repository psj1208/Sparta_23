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
    
    public int currentRound = 0;
    public Vector2 playerSpawnPosition;
    public List<E_StageType> selectedStages = new List<E_StageType>();
    public int basicStageDifficulty = 30;
    
    private int currentStageIndex = -1;
    private GameObject playerInstance;

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
            E_StageType randomStage = availableStages[Random.Range(0, availableStages.Count)];
            selectedStages.Add(randomStage);
        }
        
        currentStageIndex = -1;
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