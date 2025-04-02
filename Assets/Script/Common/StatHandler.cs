using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StatHandler : MonoBehaviour
{
    public Action<int> OnAtkUpdate;
    public Action<int> OnDefUpdate;
    public Dictionary<EStatType, List<TurnLimitStat>> TurnLimitStats = new Dictionary<EStatType, List<TurnLimitStat>>();
    public bool IsEnemy = false;

    [SerializeField] private StatData StatData; // 전체 Default Stat 정보
    private Dictionary<EStatType, float> currentStats = new Dictionary<EStatType, float>(); // Default Stat Dictionary

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        
    }

    void Initialize()
    {
        foreach (Stat entry in StatData.stats)
        {
            currentStats[entry.StatType] = entry.Value;
            TurnLimitStats[entry.StatType] = new List<TurnLimitStat>();
        }
        
    }

    public void AddStageDifficulty(float plusAtk)
    {
        currentStats[EStatType.Attack] += plusAtk;
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
    /// <param name="isPermanent">영구적용인지 turn만큼 적용인지</param>
    /// <param name="turn">적용되는 턴</param>
    public void ModifyStat(EStatType type, float value, bool isPermanent = true, int turn = 3)
    {
        if (!currentStats.ContainsKey(type)) return;
        currentStats[type] += value;

        if(!isPermanent)
        {
            turn = turn <= 0 ? 3 : turn;
            int Id = TurnLimitStats[type].Count;
            TurnLimitStats[type].Add(new TurnLimitStat(type, turn, value, Id));
            StartCoroutine(ApplyStatDuration(type, value, turn, Id));
        }

        switch(type)
        {
            case EStatType.Attack:
                OnAtkUpdate?.Invoke((int)currentStats[type]);
                break;
            case EStatType.Defense:
                OnDefUpdate?.Invoke((int)currentStats[type]);
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// 총 공격력 계산해주는 함수
    /// </summary>
    public float GetTotalAttack()
    {
        float addAtk = 0;
        for (int i = 0; i < TurnLimitStats[EStatType.Attack].Count; i++)
        {
            if (TurnLimitStats[EStatType.Attack][i].RemainTurns <= 0) continue;
            addAtk += TurnLimitStats[EStatType.Attack][i].RemainValue;
        }
        return currentStats[EStatType.Attack] + addAtk;
    }

    /// <summary>
    /// 턴 제한 스탯 중 먼저 얻은 스탯의 수치를 소모
    /// </summary>
    /// <param name="type">소모할 스탯 타입</param>
    /// <param name="value">소모해야 하는 양</param>
    /// <returns>소모하고 남은 양</returns>
    public float ReduceStatFIFO(EStatType type, float value)
    {
        // 순차적으로 value 털기
        for (int i = 0; i < TurnLimitStats[type].Count; i++)
        {
            if (TurnLimitStats[type][i].RemainValue >= value)
            {
                TurnLimitStats[type][i].RemainValue -= value;
                value = 0;
                break;
            }
            else
            {
                value = value - TurnLimitStats[type][i].RemainValue;
                TurnLimitStats[type][i].RemainValue = 0;
            }

            if (value <= 0) break;
        }
        return value;
    }

    
    IEnumerator ApplyStatDuration(EStatType type, float value, float turn, int coroutineID)
    {
        while (TurnLimitStats[type][coroutineID].RemainTurns > 0)
        {
            yield return null;
        }

        currentStats[type] -= TurnLimitStats[type][coroutineID].RemainValue; 

        if (currentStats[type] < 0) currentStats[type] = 0;

        switch (type)
        {
            case EStatType.Attack:
                OnAtkUpdate?.Invoke((int)currentStats[type]);
                break;
            case EStatType.Defense:
                OnDefUpdate?.Invoke((int)currentStats[type]);
                break;
            default:
                break;
        }
    }
}
