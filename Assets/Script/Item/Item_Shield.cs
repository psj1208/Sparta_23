using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : MonoBehaviour, IItem
{
    public string ItemName { get; }
    public float baseValue;

    public void UseItem(Player player)
    {
        if (player != null)
        {
            player.StatHandler.ModifyStat(EStatType.Defense, baseValue, false, 0);
        }
    }
}
