using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HealPotion : MonoBehaviour, IItem
{
    public string ItemName { get; }
    private float baseValue;

    public void UseItem(Player player)
    {
        if (player == null)
            player.ResourceController.ChangeHealth(baseValue);
    }
}
