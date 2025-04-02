using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object / New Item")]

public class ItemSO : ScriptableObject
{
    [SerializeField] private string itemName;
    public string Description;
    [SerializeField] public Sprite sprite;
    [SerializeField] private EItemType itemType;
    [SerializeField] private int value;
    [SerializeField] public GameObject itemPrefab;
    [SerializeField] public bool stackable = true;
    public int BuyPrice;

    public ItemSO(string name, Sprite icon, bool stackable, int value)
    {
        this.itemName = name;
        this.sprite = icon;
        this.stackable = stackable;
        this.value = value;
    }

    public string ItemName => itemName;
    public void UseItem(Player player)//PlayerController player
    {
        switch (itemType)
        {
            case EItemType.Attack:
                player.StatHandler.ModifyStat(EStatType.Attack, value, false, 0);
                break;

            case EItemType.Defense:
                player.StatHandler.ModifyStat(EStatType.Defense, value, false, 0);
                break;

            case EItemType.Heal:
                player.ResourceController.ChangeHealth(value);
                break;

            case EItemType.Gold:
                //TODO: 플레이어 골드 획득
                //player.ResourceController.GetGold(value);
                break;
        }
    }
}
