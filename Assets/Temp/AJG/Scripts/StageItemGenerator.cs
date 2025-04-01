using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageItemGenerator : MonoBehaviour
{
    public GameObject[] StageItemPrefab;
    public Transform generatePosition;
    public int generateAmount = 10;

    private void Start()
    {
        StartStageItemGenerate(generateAmount);
    }

    public void StartStageItemGenerate(int generateAmount = 5)
    {
        for (int i = 0; i < generateAmount; i++)
        {
            float randomX = Random.Range(-1f, 1f);
            Vector3 spawnPosition = generatePosition.position + new Vector3(randomX, 0f, 0f);
            
            GameObject randomPrefab = StageItemPrefab[Random.Range(0, StageItemPrefab.Length)];

            Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
