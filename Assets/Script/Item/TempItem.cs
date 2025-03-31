using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempItem : MonoBehaviour, IItem
{
    public float value;
    public string ItemName { get; }
    public void UseItem(Player player)
    {
        player.StatHandler.ModifyStat(EStatType.Attack, value, true, 0);
    }
}
