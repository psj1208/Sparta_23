using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleStageController : MonoBehaviour
{
    public List<EnemyData> allEnemies;
    public Vector2 enemySpawnCenter;
    public Vector2 playerSpawnPosition;
    public List<Enemy> spawnedEnemies = new List<Enemy>();
    public float spacing = 2f;
    private int currentStageDifficulty;
    
    public void StartBattleStage(GameObject playerPrefab)
    {
        ClearData();
        SpawnPlayer(playerPrefab, playerSpawnPosition);
        currentStageDifficulty = StageManager.Instance.basicStageDifficulty + (StageManager.Instance.currentRound * 20);
        SpawnEnemies(enemySpawnCenter, currentStageDifficulty);
    }

    private void ClearData()
    {
        spawnedEnemies.Clear();
    }
    
    void SpawnPlayer(GameObject playerPrefab, Vector2 position)
    {
        GameObject playerInstance = Instantiate(playerPrefab, position, Quaternion.identity);
        Player player = playerInstance.GetComponent<Player>();
        GameManager.Instance.Player = player;
    }
    
    void SpawnEnemies(Vector2 center, int stageDifficulty)
    {
        int remainingDifficulty = stageDifficulty;
        List<EnemyData> possibleEnemies = new List<EnemyData>(allEnemies);
        while (remainingDifficulty > 0)
        {
            var validEnemies = possibleEnemies.Where(e => e.power <= remainingDifficulty).ToList();

            if (validEnemies.Count == 0)
                break;

            EnemyData selectedEnemy = validEnemies[Random.Range(0, validEnemies.Count)];

            remainingDifficulty -= selectedEnemy.power;

            GameObject obj = Instantiate(selectedEnemy.prefab, center, Quaternion.identity);
            Enemy enemy = obj.GetComponent<Enemy>();
            enemy.BattleStageController = this;
            enemy.StatHandler.AddStageDifficulty(StageManager.Instance.currentRound * 5);
            spawnedEnemies.Add(enemy);
        }
        ArrangeEnemies(spawnedEnemies, center);
    }

    void ArrangeEnemies(List<Enemy> enemies, Vector2 center)
    {
        int count = enemies.Count;
        for (int i = 0; i < count; i++)
        {
            float offset = GetOffset(i, count);
            Vector2 spawnPosition = new Vector2(center.x + offset, center.y);
            Debug.Log(spawnPosition);
            enemies[i].transform.position = spawnPosition;
        }
    }

    float GetOffset(int index, int count)
    {
        if (count == 1) return 0;
        return (index - (count - 1) / 2f) * spacing;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }

        if (spawnedEnemies.Count <= 0)
        {
            UIManager.Show<UIReward>(ResourceObjectUtil.ShowRandomObjects<ItemSO>("Assets/Script/Scriptable/ScriptableObject(item)", 3));
        }
    }
}