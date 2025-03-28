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

    /// <summary>
    /// Stat 값을 변경 가능한 함수. 단, 이곳에서 체력은 MaxHealth값입니다. 현재 체력 변경은 ResourceController.cs
    /// </summary>
    /// <param name="type">변경할 스탯의 타입</param>
    /// <param name="value">값</param>
    /// <param name="isPermanent">영구적인 변화인지, 일시 효과인지</param>
    /// <param name="turnTime">일시 효과 적용 시간</param>
    public void ModifyStat(EStatType type, float value, bool isPermanent, int turnTime)
    {
        if (!currentStats.ContainsKey(type)) return;
        currentStats[type] += value;

        if(!isPermanent)
        {
            // TODO : 일시적 효과 적용
        }
    }
}
