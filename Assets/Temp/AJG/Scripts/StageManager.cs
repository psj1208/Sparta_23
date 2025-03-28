using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public GameObject playerPrefab;
    public SceneLoader sceneLoader;
    public List<EnemyData> allEnemies;
    public int basicStageDifficulty = 30;
    public Vector2 enemySpwanCenterPosition;
    public Vector2 playerSpawnPosition;
    public float spacing = 2f;

    public Dictionary<E_StageType, float> stagePoints;
    public List<E_StageType> selectedStages = new List<E_StageType>();
    private int currentStageIndex = 0;

    public int currentStage = 0;
    public int currentStageDifficulty;
    private GameObject playerInstance;
    
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
        if (scene.name == "StageTestScene") 
        {
            StartBattleStage();
        }
    }
    
    public void NextStage()
    {   
        Debug.Log("currentStageIndex : " + currentStageIndex );
        if (currentStageIndex >= selectedStages.Count)
        {
            sceneLoader.ReturnToStageSelectScene();
        }
        else
        {
            sceneLoader.LoadStage(selectedStages[currentStageIndex]);
        }
        currentStageIndex++;
    }
    
    void StartBattleStage()
    {
        currentStage++;
        SpawnPlayer();
        currentStageDifficulty = basicStageDifficulty + (currentStage * 5);
        SpawnEnemies(enemySpwanCenterPosition, currentStageDifficulty);
    }
    
    void SpawnPlayer()
    {
        if (playerInstance != null)
            Destroy(playerInstance);

        playerInstance = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
    }
    
    public void ClearStage()
    {
        currentStageIndex++;
        if (currentStageIndex >= selectedStages.Count)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        else
        {
            SceneManager.LoadScene("StageTestScene");
        }
    }

    public void SelectStages()
    {
        selectedStages.Clear();
        List<E_StageType> availableStages = stagePoints.Keys.ToList();
        for (int i = 0; i < 3; i++)
        {
            E_StageType randomStage = availableStages[Random.Range(0, availableStages.Count)];
            selectedStages.Add(randomStage);
        }
        currentStageIndex = 0;
    }
    
    void SpawnEnemies(Vector2 center, int stageDifficulty)
    {
        int remainingDifficulty = stageDifficulty;
        List<EnemyData> possibleEnemies = new List<EnemyData>(allEnemies);
        List<GameObject> spawnedEnemies = new List<GameObject>();

        while (remainingDifficulty > 0)
        {
            var validEnemies = possibleEnemies.Where(e => e.power <= remainingDifficulty).ToList();

            if (validEnemies.Count == 0)
                break;

            EnemyData selectedEnemy = validEnemies[Random.Range(0, validEnemies.Count)];

            remainingDifficulty -= selectedEnemy.power;

            GameObject enemy = Instantiate(selectedEnemy.prefab, center, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }

        ArrangeEnemies(spawnedEnemies, center);
    }

    void ArrangeEnemies(List<GameObject> enemies, Vector2 center)
    {
        int count = enemies.Count;
        for (int i = 0; i < count; i++)
        {
            float offset = GetOffset(i, count);
            Vector2 spawnPosition = new Vector2(center.x + offset, center.y);
            enemies[i].transform.position = spawnPosition;
        }
    }

    float GetOffset(int index, int count)
    {
        if (count == 1) return 0;
        return (index - (count - 1) / 2f) * spacing;
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