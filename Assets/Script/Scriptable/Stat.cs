using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat : ScriptableObject
{
    public EStatType StatType;
    public float Value;
}

[CreateAssetMenu(fileName = "New StatData", menuName = "Stats/Character Stats")]
public class StatData : ScriptableObject
{
    public string CharacterName;
    public List<Stat> stats;
}
