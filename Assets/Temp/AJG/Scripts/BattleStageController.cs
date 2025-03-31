using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleStageController : MonoBehaviour
{
    public List<EnemyData> allEnemies;
    public Vector2 enemySpawnCenter;
    public Vector2 playerSpawnPosition;
    public float spacing = 2f;
    private int currentStageDifficulty;
    
    public void StartBattleStage(GameObject playerPrefab)
    {
        SpawnPlayer(playerPrefab, playerSpawnPosition);
        currentStageDifficulty = StageManager.Instance.basicStageDifficulty + (StageManager.Instance.currentRound * 20);
        SpawnEnemies(enemySpawnCenter, currentStageDifficulty);
    }

    void SpawnPlayer(GameObject playerPrefab, Vector2 position)
    {
        GameObject playerInstance = Instantiate(playerPrefab, position, Quaternion.identity);
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
}