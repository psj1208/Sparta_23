using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object / New Item")]

public class ItemSO : ScriptableObject, IItem
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private EItemType itemType;
    [SerializeField] private int value;
    [SerializeField] private GameObject itemPrefab;

    public string ItemName => itemName;
    public void UseItem()//PlayerController player
    {
        switch (itemType)
        {
            case EItemType.Attack:
                //TODO: 플레이어 공격
                //player.UpdateStats(value)
                break;

            case EItemType.Defense:
                //TODO: 플레이어 방어
                //player.UpdateStats(value)
                break;

            case EItemType.Heal:
                //TODO: 플레이어 회복
                //player.UpdateHealth(value)
                break;
        }
    }

}
