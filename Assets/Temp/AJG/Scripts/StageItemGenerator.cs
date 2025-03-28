using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItemGenerator : MonoBehaviour
{
    public GameObject[] StageItemPrefab;
    public Transform generatePosition;

    public void StartStageItemGenerate()
    {
        for (int i = 0; i < 5; i++)
        {
            float randomX = Random.Range(-1f, 1f);
            Vector3 spawnPosition = generatePosition.position + new Vector3(randomX, 0f, 0f);
            
            GameObject randomPrefab = StageItemPrefab[Random.Range(0, StageItemPrefab.Length)];

            Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
