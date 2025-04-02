using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EStatType
{
    Health, 
    Attack,
    Defense,
}

public enum EItemType
{
    Attack,
    Defense,
    Heal,
    Gold
}

public enum ETurnState
{
    PlayerTurn,
    EnemyTurn,
    ClawTurn
}

public enum ESFXType
{
    Attack,
    Damaged,
    GoodEffect,
    UIClick,
    Clear1,
}