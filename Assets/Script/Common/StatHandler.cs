using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private StatData StatData; // 전체 Default Stat 정보
    private Dictionary<EStatType, float> currentStats = new Dictionary<EStatType, float>(); // Default Stat Dictionary

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        foreach (Stat entry in StatData.stats)
        {
            currentStats[entry.StatType] = entry.Value;
        }
    }

    public float GetStat(EStatType type)
    {
        return currentStats.ContainsKey(type) ? currentStats[type] : 0;
    }

    public void ModifyValue(EStatType type, float value, bool isPermanent, int turnTime)
    {
        if (!currentStats.ContainsKey(type)) return;
        currentStats[type] += value;

        if(!isPermanent)
        {
            // TODO : 일시적 효과 적용
        }
    }
}
