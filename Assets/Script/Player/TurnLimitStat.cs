using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLimitStat 
{
    public EStatType StatType;
    public int RemainTurns;
    public float RemainValue;
    public int CoroutineID;

    public TurnLimitStat(EStatType statType, int remainTurns, float remainValue, int coroutineID)
    {
        StatType = statType;
        RemainTurns = remainTurns;
        RemainValue = remainValue;
        CoroutineID = coroutineID;
    }
}
