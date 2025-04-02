using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public Action<int> OnAtkUpdate;
    public Action<int> OnDefUpdate;
    public List<int> Turns = new List<int>();
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
            Turns.Add(turn);
            StartCoroutine(ApplyStatDuration(type, value, turn, Turns.Count - 1));
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
        return currentStats[EStatType.Attack];
    }

    IEnumerator ApplyStatDuration(EStatType type, float value, float turn, int coroutineID)
    {
        while(Turns[coroutineID] > 0)
        {
            yield return null;
        }

        currentStats[type] -= value;
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
