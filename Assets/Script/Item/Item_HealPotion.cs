using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HealPotion : MonoBehaviour, IItem
{
    public string ItemName { get; }
    public float baseValue;

    public void UseItem(Player player)
    {
        if (player != null)
        {
            Debug.Log(baseValue);
            player.ResourceController.ChangeHealth(baseValue);
        }
    }
}
