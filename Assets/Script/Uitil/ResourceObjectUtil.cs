using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ResourceObjectUtil
{
    public static List<T> ShowRandomObjects<T>(string folderPath, int count) where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { folderPath });
        List<T> validAssets = new List<T>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);

            if (asset != null)
            {
                // 예외처리: 특정 필드 값이 null 또는 비어있는 경우 제외
                if (IsValid(asset))
                {
                    validAssets.Add(asset);
                }
            }
        }

        if (validAssets.Count < count)
        {
            return new List<T>(validAssets); // 전부 반환
        }

        Shuffle(validAssets);
        return validAssets.GetRange(0, count);
    }


    private static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    // ✅ 유효성 검사: 원하는 필드에 맞게 커스터마이징
    private static bool IsValid<T>(T obj) where T : ScriptableObject
    {
        // 예: MyData 타입에 dataName이라는 string 필드가 있고 그게 null/빈 문자열이면 false
        if (obj is ItemSO item)
        {
            return item.itemPrefab != null;
        }

        // 기본적으로는 모두 유효하다고 판단
        return true;
    }
}
